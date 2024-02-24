using Gravity.Express.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gravity.Express.Infrastructure.Persistence;

public class GravityExpressDbContext : DbContext, IGravityExpressDbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Delivery> Deliveries { get; set; }

    public DbSet<ECommercePlatform> ECommercePlatforms { get; set; }

    public DbSet<AuditLog> AuditLogs { get; set; }

    private int TenantId = 0;

    public GravityExpressDbContext(DbContextOptions<GravityExpressDbContext> options, IContextWrapper contextWrapper) : base(options)
    {
        TenantId = contextWrapper.GetTenantId();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
