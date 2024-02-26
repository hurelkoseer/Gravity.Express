using Gravity.Express.Domain.Common;
using Gravity.Express.Domain.Enums;

namespace Gravity.Express.Domain.Entities;

public class Delivery : EntityBase, IMultiTenant
{
    public required Guid CustomerId { get; set; }

    public required string SenderWarehouseAddress { get; set; }

    public required string DeliveryAddress { get; set; }

    public required string TrackingNumber { get; set; }

    public int RetryCount { get; set; }

    public required DeliveryStatus Status { get; set; }

    public required SyncState SyncState { get; set; }

    public Customer Customer { get; set; }

    public int TenantId { get; private set; }

    public void SetTenantId(int tenantId)
    {
        TenantId = tenantId;
    }
}
