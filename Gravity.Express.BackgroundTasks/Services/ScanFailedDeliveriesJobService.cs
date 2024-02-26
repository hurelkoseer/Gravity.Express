using Gravity.Express.Domain.Enums;
using Gravity.Express.Infrastructure.Persistence;
using Gravity.Express.IntegrationEvent.Deliveries;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Gravity.Express.BackgroundTasks.Services;

public class ScanFailedDeliveriesJobService : BackgroundService
{
    private readonly TimeSpan _timerPeriod = TimeSpan.FromSeconds(30);

    private readonly IServiceProvider _serviceProvider;

    private readonly ILogger<ScanFailedDeliveriesJobService> _logger;

    public ScanFailedDeliveriesJobService(IServiceProvider serviceProvider,
                                            ILogger<ScanFailedDeliveriesJobService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_timerPeriod);

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await CheckFailDeliveryRetriesAsync(stoppingToken);
        }
    }

    private async Task CheckFailDeliveryRetriesAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

        var failedDeliveries = await dbContext.Deliveries.IgnoreQueryFilters()
                                                .Where(x => x.SyncState == SyncState.Failed && x.RetryCount <= 3)
                                                .ToListAsync(cancellationToken: stoppingToken)!;

        foreach (var deliveryRetry in failedDeliveries)
        {
            _logger.LogInformation("Re-Run publishing {Event}. Delivery Id: {DeliveryId}",
                                   nameof(DeliveryCreatedIntegrationEvent), deliveryRetry.Id);

            deliveryRetry.SyncState = SyncState.Inqueue;
            await dbContext.SaveChangesAsync(stoppingToken);

            await publishEndpoint!
                  .Publish(new DeliveryCreatedIntegrationEvent { DeliveryId = deliveryRetry.Id },
                           stoppingToken)
                  .ConfigureAwait(false);

            _logger.LogInformation("Re-Run published {Event}. Delivery Id: {DeliveryId}",
                                   nameof(DeliveryCreatedIntegrationEvent), deliveryRetry.Id);
        }
    }
}
