using Gravity.Express.Domain.Common;

namespace Gravity.Express.Domain.Entities;

public class Customer : EntityBase, IMultiTenant
{
    public required string Name { get; set; }

    public int TenantId { get; private set; }

    public void SetTenantId(int tenantId)
    {
        TenantId = tenantId;
    }

    public List<Delivery> Deliveries { get; set; } = new();

}
