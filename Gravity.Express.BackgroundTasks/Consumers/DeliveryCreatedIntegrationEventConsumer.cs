using Gravity.Express.Domain.Enums;
using Gravity.Express.Infrastructure.Persistence;
using Gravity.Express.IntegrationEvent.Deliveries;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gravity.Express.BackgroundTasks.Consumers;

public class DeliveryCreatedIntegrationEventConsumer : IConsumer<DeliveryCreatedIntegrationEvent>
{
    private readonly IAppDbContext _dbContext;

    private readonly ILogger<DeliveryCreatedIntegrationEventConsumer> _logger;

    public DeliveryCreatedIntegrationEventConsumer(ILogger<DeliveryCreatedIntegrationEventConsumer> logger,
                                                   IAppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<DeliveryCreatedIntegrationEvent> context)
    {
        var @event = context.Message;

        var delivery = await _dbContext.Deliveries
                                          .IgnoreQueryFilters()
                                          .SingleOrDefaultAsync(q => q.Id == @event.DeliveryId);

        if (delivery == null)
        {
            _logger.LogError(
                "A Delivery couldn't found in the database. DeliveryId= {DeliveryId}.", @event.DeliveryId);

            return;
        }

        if (delivery.RetryCount >= 3)
        {
            _logger.LogWarning(
                "Skipped delivery due to it's exceed retry limit. Payload = {payload}.",
                new { delivery.Id, delivery.SyncState, delivery.RetryCount });

            return;
        }

        if (delivery.SyncState != SyncState.Inqueue)
        {
            _logger.LogWarning(
                "Skipped delivery due to it's state. Payload = {payload}.",
                new { delivery.Id, delivery.SyncState });

            return;
        }

        delivery.SyncState = SyncState.Success;
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        _logger.LogInformation(
            "A Delivery has been sent to legacy. Delivery= {DeliveryId}.", delivery.Id);
    }
}
