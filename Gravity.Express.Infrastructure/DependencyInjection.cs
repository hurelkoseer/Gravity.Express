using Audit.Core;
using Gravity.Express.Domain.Common;
using Gravity.Express.Domain.Entities;
using Gravity.Express.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using DataType = Audit.PostgreSql.Configuration.DataType;

namespace Gravity.Express.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddPostgresql(services, configuration);
        Configuration.Setup()
                     .UsePostgreSql(config => config
                                              .ConnectionString(configuration["ConnectionString"])
                                              .TableName("AuditLogs")
                                              .IdColumnName("Id")
                                              .DataColumn("Data", DataType.JSON)
                                              .LastUpdatedColumnName("LastUpdatedDate"));

        Configuration.Setup()
                     .UseEntityFramework(ef => ef
                                               .AuditTypeMapper(_ => typeof(AuditLog))
                                               .AuditEntityAction<AuditLog>((evt, entry, auditEntity) =>
                                               {
                                                   var entityEntry = entry.GetEntry();
                                                   var value = entry.Action switch
                                                   {
                                                       "Insert" => entry.ColumnValues.ToDictionary(
                                                           kvp => kvp.Key, kvp => kvp.Value),
                                                       "Update" when entry.Changes != null => entry.Changes
                                                           .ToDictionary(
                                                               change => change.ColumnName,
                                                               change => (object)new
                                                               {
                                                                   OldValue = change.OriginalValue,
                                                                   NewValue = change.NewValue
                                                               }),
                                                       _ => new Dictionary<string, object>()
                                                   };

                                                   auditEntity.Data = JsonConvert.SerializeObject(new
                                                   {
                                                       EntityName = entityEntry.Entity.GetType().Name,
                                                       Action = entry.Action,
                                                       Data = value
                                                   });

                                                   auditEntity.LastUpdatedDate = DateTime.Now;

                                                   if (entityEntry.Entity is IMultiTenant multiTenantEntity)
                                                   {
                                                       auditEntity.SetTenantId(multiTenantEntity.TenantId);
                                                   }
                                               })
                                               .IgnoreMatchedProperties(true));

        return services;
    }

    private static void AddPostgresql(IServiceCollection services,
                                      IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var connectionString = configuration["ConnectionString"];

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure();
                });

           // options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
    }
}
