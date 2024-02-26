using Gravity.Express.Application.Cqrs.Customer.Commands.CreateCustomer;
using Gravity.Express.Application.Exceptions;
using Gravity.Express.Domain.Enums;
using Gravity.Express.Infrastructure.Persistence;
using Gravity.Express.IntegrationEvent.Deliveries;
using MassTransit;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Gravity.Express.Application.Cqrs.Delivery.Commands.CreateDelivery;

public class CreateDeliveryCommandHandler : IRequestHandler<CreateDeliveryCommand, CreateDeliveryCommandResponse>
{
    private readonly IAppDbContext _appDbContext;

    private readonly IPublishEndpoint _publishEndpoint;

    private readonly ILogger<CreateCustomerCommandHandler> _logger;

    public CreateDeliveryCommandHandler(IAppDbContext appDbContext,
                                        ILogger<CreateCustomerCommandHandler> logger,
                                        IPublishEndpoint publishEndpoint)
    {
        _appDbContext = appDbContext;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async ValueTask<CreateDeliveryCommandResponse> Handle(CreateDeliveryCommand request,
                                                                 CancellationToken cancellationToken)
    {
        var isExistsDelivery =
            await _appDbContext.Deliveries.AnyAsync(x => x.DeliveryAddress == request.DeliveryAddress &&
                                                         x.TrackingNumber == request.TrackingNumber &&
                                                         x.SenderWarehouseAddress == request.senderWarehouseAddress &&
                                                         x.CustomerId == request.CustomerId,
                                                    cancellationToken: cancellationToken);

        if (isExistsDelivery)
        {
            throw new ValidationFailedException("The delivery is already exists.");
        }

        var delivery = new Domain.Entities.Delivery
        {
            DeliveryAddress = request.DeliveryAddress,
            TrackingNumber = request.TrackingNumber,
            SenderWarehouseAddress = request.senderWarehouseAddress,
            CustomerId = request.CustomerId,
            SyncState = SyncState.Inqueue,
            Status = DeliveryStatus.Pending,
            IsPassive = request.IsPassive
        };

        _appDbContext.Deliveries.Add(delivery);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        await PublishIntegrationEvent(delivery, cancellationToken);

        return new CreateDeliveryCommandResponse(delivery.Id);
    }

    private async Task PublishIntegrationEvent(Domain.Entities.Delivery delivery,
                                               CancellationToken cancellationToken)
    {
        _logger.LogInformation("Publishing {Event}. Delivery Id: {DeliveryId}", nameof(DeliveryCreatedIntegrationEvent),
                               delivery.Id);

        await _publishEndpoint.Publish(
            new DeliveryCreatedIntegrationEvent { DeliveryId = delivery.Id },
            cancellationToken);

        delivery.SyncState = SyncState.Inqueue;

        await _appDbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Published {Event}. Delivery Id: {DeliveryId}", nameof(DeliveryCreatedIntegrationEvent),
                               delivery.Id);
    }
}
