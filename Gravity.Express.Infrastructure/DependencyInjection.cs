using System.ComponentModel.DataAnnotations;
using Audit.Core;
using Audit.PostgreSql.Configuration;
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
                                              .TableName("audit_logs")
                                              .IdColumnName("id")
                                              .DataColumn("data", DataType.JSON)
                                              .LastUpdatedColumnName("last_updated_date"));

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

        services.AddDbContext<GravityExpressDbContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(GravityExpressDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure();
                });

            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IGravityExpressDbContext>(provider => provider.GetRequiredService<GravityExpressDbContext>());
    }
}
