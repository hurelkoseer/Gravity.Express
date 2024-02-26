using Microsoft.Extensions.DependencyInjection;

namespace Gravity.Express.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient);

        //  services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        //  services.AddSingleton<ITenantMapper, TenantMapper>();
        return services;
    }
}
