using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BinBin.Partnership.API.Filters;

public class MultiTenantActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var tenantInfo = context.HttpContext.GetMultiTenantContext<TenantInfo>();

        if (tenantInfo is { HasResolvedTenant: true })
        {
            return;
        }

        var modelState = new ModelStateDictionary();
        modelState.AddModelError("TenantNotFound", "The requested tenant does not exist.");
        context.Result = new BadRequestObjectResult(modelState);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}