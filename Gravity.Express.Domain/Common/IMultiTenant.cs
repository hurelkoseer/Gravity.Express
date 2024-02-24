namespace Gravity.Express.Domain.Common;

public interface IMultiTenant
{
    public int TenantId { get; }

    void SetTenantId(int tenantId);
}