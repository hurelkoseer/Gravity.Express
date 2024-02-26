using System.Reflection;
using Gravity.Express.BackgroundTasks.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gravity.Express.BackgroundTasks.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomMassTransit(this IServiceCollection services,
                                                          IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(Assembly.GetExecutingAssembly());

            x.UsingRabbitMq((context, cfg) =>
            {
                var hostAndPort = $"rabbitmq://127.0.0.1:5672";

                cfg.Host(hostAndPort, h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services.Configure<MassTransitHostOptions>(options => { options.WaitUntilStarted = true; });

        return services;
    }

    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services,
                                                        IConfiguration configuration)
    {
        services.AddHostedService<ScanFailedDeliveriesJobService>();
        return services;
    }
}
