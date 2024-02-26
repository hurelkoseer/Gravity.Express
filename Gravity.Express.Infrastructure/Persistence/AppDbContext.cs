using System.Reflection;
using Audit.EntityFramework;
using Gravity.Express.Domain.Common;
using Gravity.Express.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gravity.Express.Infrastructure.Persistence;

public class AppDbContext : AuditDbContext, IAppDbContext
{
    public DbSet<Delivery> Deliveries { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<AuditLog> AuditLogs { get; set; }

    private int? _tenantId;

    private readonly IContextWrapper _contextWrapper;

    public AppDbContext(DbContextOptions<AppDbContext> options, IContextWrapper contextWrapper) : base(options)
    {
        _contextWrapper = contextWrapper;
    }

    public int TenantId => _tenantId ??= _contextWrapper.GetTenantId();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.Now;

                    if (entry.Entity is IMultiTenant entity)
                    {
                        if (TenantId < 1)
                        {
                            throw new ArgumentOutOfRangeException(nameof(TenantId), TenantId.ToString());
                        }

                        if (entity.TenantId == default)
                        {
                            entity.SetTenantId(TenantId);
                        }
                    }

                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.Now;

                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Delivery>().HasQueryFilter(x => x.TenantId == TenantId && x.IsDelete == false);
        modelBuilder.Entity<Customer>().HasQueryFilter(x => x.TenantId == TenantId && x.IsDelete == false);
        base.OnModelCreating(modelBuilder);
    }
}
