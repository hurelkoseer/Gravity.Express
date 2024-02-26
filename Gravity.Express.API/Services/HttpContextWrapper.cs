using Finbuckle.MultiTenant;
using Gravity.Express.Infrastructure.Persistence;

namespace Gravity.Express.API.Services;

public sealed class HttpContextWrapper : IContextWrapper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private int _tenantId;

    public HttpContextWrapper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetTenantId()
    {
        if (_tenantId > 0)
        {
            return _tenantId;
        }

        if (_httpContextAccessor.HttpContext==null)
        {
            return _tenantId;
        }

        var context = _httpContextAccessor.HttpContext?.GetMultiTenantContext<TenantInfo>();

        if (context is not { HasResolvedTenant : true })
        {
            throw new ApplicationException("TenantNotFound");
        }

        _tenantId = int.Parse(context!.TenantInfo!.Id!);

        return _tenantId;
    }
}
