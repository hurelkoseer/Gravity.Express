using Finbuckle.MultiTenant;
using Gravity.Express.Application.Exceptions;
using Gravity.Express.Application.Model;
using Microsoft.AspNetCore.Http;

namespace Gravity.Express.Application.Mapping;

public class TenantMapper : ITenantMapper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantMapper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GetTenantNameAsync(int tenantId)
    {
        var tenantInfo = await _httpContextAccessor.HttpContext!.GetMultiTenantContext<TenantInfo>()!.StoreInfo!.Store!
                                                   .TryGetAsync(tenantId.ToString());

        if (tenantInfo == null)
        {
            throw new NotFoundException(nameof(tenantInfo), tenantId.ToString());
        }

        return tenantInfo.Name!;
    }

    public string GetTenantName(int tenantId)
    {
        var tenantInfo = _httpContextAccessor.HttpContext!.GetMultiTenantContext<TenantInfo>()!.StoreInfo!.Store!
                                             .TryGetAsync(tenantId.ToString()).GetAwaiter().GetResult();

        if (tenantInfo == null)
        {
            throw new NotFoundException(nameof(tenantInfo), tenantId.ToString());
        }

        return tenantInfo.Name!;
    }

    public async Task<bool> IsExistAsync(int tenantId)
    {
        var tenantInfo = await _httpContextAccessor.HttpContext!.GetMultiTenantContext<TenantInfo>()!.StoreInfo!.Store!
                                                   .TryGetAsync(tenantId.ToString());

        return tenantInfo != null;
    }

    public async Task<List<TenantInfo>> GetTenantsAsync()
    {
        return (await _httpContextAccessor.HttpContext!.GetMultiTenantContext<TenantInfo>()!.StoreInfo!.Store!
                                          .GetAllAsync()).ToList();
    }

    public async Task<List<EntityItem<int>>?> MapTenantsAsync(List<int>? partnerTenants)
    {
        var response = new List<EntityItem<int>>();

        if (partnerTenants == null)
        {
            return response;
        }

        var tenants = await GetTenantsAsync();

        foreach (var partnerTenant in partnerTenants)
        {
            var tenantInfo = tenants.FirstOrDefault(q => q.Id == partnerTenant.ToString());

            if (tenantInfo != null)
            {
                response.Add(new EntityItem<int>(int.Parse(tenantInfo.Id!), tenantInfo.Name!));
            }
        }

        return response;
    }

    public List<EntityItem<int>> MapTenants(List<int>? partnerTenants)
    {
        var response = new List<EntityItem<int>>();

        if (partnerTenants == null)
        {
            return response;
        }

        var tenants = GetTenants();

        foreach (var partnerTenant in partnerTenants)
        {
            var tenantInfo = tenants.FirstOrDefault(q => q.Id == partnerTenant.ToString());

            if (tenantInfo != null)
            {
                response.Add(new EntityItem<int>(int.Parse(tenantInfo.Id!), tenantInfo.Name!));
            }
        }

        return response;
    }

    private List<TenantInfo> GetTenants()
    {
        return _httpContextAccessor.HttpContext!.GetMultiTenantContext<TenantInfo>()!.StoreInfo!.Store!
                                   .GetAllAsync().GetAwaiter().GetResult().ToList();
    }
}
