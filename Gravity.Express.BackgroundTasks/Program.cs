using Gravity.Express.BackgroundTasks.Extensions;
using Gravity.Express.Infrastructure;
using Gravity.Express.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


using var host = Host.CreateDefaultBuilder(args)
                     .ConfigureServices((hostContext, services) =>
                     {
                         services.AddOptions();
                         services.AddCustomMassTransit(hostContext.Configuration);
                         services.AddInfrastructure(hostContext.Configuration);
                         services.AddScoped<IContextWrapper, NullContextWrapper>();
                         services.AddHttpContextAccessor();
                         services.AddBackgroundTasks(hostContext.Configuration);
                     })
                     .Build();
await host.RunAsync();
