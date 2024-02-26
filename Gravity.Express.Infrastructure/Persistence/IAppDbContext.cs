using Gravity.Express.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gravity.Express.Infrastructure.Persistence;

public interface IAppDbContext
{
    DbSet<Delivery> Deliveries { get; set; }

    DbSet<Customer> Customers { get; set; }

    DbSet<AuditLog> AuditLogs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
