using Microsoft.Extensions.DependencyInjection;

namespace Gravity.Express.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient);
        return services;
    }
}
