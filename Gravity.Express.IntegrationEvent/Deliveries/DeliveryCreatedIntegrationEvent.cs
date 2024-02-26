namespace Gravity.Express.IntegrationEvent.Deliveries;

public sealed record DeliveryCreatedIntegrationEvent
{
    public Guid DeliveryId { get; init; }
}
