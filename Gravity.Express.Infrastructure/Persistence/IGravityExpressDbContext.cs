using Gravity.Express.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gravity.Express.Infrastructure.Persistence;

public interface IGravityExpressDbContext
{
    DbSet<Customer> Customers { get; set; }

    DbSet<Order> Orders { get; set; }

    DbSet<Delivery> Deliveries { get; set; }

    DbSet<ECommercePlatform> ECommercePlatforms { get; set; }

    DbSet<AuditLog> AuditLogs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
