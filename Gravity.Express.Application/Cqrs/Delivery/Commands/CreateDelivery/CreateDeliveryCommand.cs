using Mediator;

namespace Gravity.Express.Application.Cqrs.Delivery.Commands.CreateDelivery;

public class CreateDeliveryCommand : IRequest<CreateDeliveryCommandResponse>
{
    public required string DeliveryAddress { get; set; }
    public required string senderWarehouseAddress { get; set; }
    public required string TrackingNumber { get; set; }
    public required Guid CustomerId { get; set; }

    public required bool IsPassive { get; set; }
}
