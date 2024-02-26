namespace Gravity.Express.API.Models;

public class CreateDeliveryRequest
{
    public required string DeliveryAddress { get; set; }

    public required string senderWarehouseAddress { get; set; }

    public required string TrackingNumber { get; set; }

    public required Guid CustomerId { get; set; }

    public bool? IsPassive { get; set; }
}
