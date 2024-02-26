using Finbuckle.MultiTenant;
using Gravity.Express.Application.Model;

namespace Gravity.Express.Application.Mapping;

public interface ITenantMapper
{
    Task<string> GetTenantNameAsync(int tenantId);
    string GetTenantName(int tenantId);
    Task<bool> IsExistAsync(int tenantId);
    Task<List<TenantInfo>> GetTenantsAsync();
    Task<List<EntityItem<int>>?> MapTenantsAsync(List<int>? partnerTenants);

    List<EntityItem<int>> MapTenants(List<int>? partnerTenants);
}
