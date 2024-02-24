using Gravity.Express.Domain.Common;

namespace Gravity.Express.Domain.Entities;

public class AuditLog : IMultiTenant
{
    public int Id { get; set; }

    public string Data { get; set; }

    public DateTime LastUpdatedDate { get; set; }

    public int TenantId { get; set; }

    public void SetTenantId(int tenantId)
    {
        TenantId = tenantId;
    }
}
