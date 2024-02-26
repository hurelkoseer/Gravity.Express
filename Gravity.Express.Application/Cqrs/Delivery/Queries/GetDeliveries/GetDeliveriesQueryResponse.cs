namespace Gravity.Express.Application.Cqrs.Delivery.Queries.GetDeliveries;

public sealed record GetDeliveriesQueryResponse(Guid Id, string DeliveryAddress, string SenderWarehouseAddress,
                                                string TrackingNumber, int TenantId, bool IsPassive);
