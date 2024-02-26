using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gravity.Express.Infrastructure.Persistence;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=GravityExpressDB;Username=gravity;Password=express");

        var contextWrapper = new NullContextWrapper();

        return new AppDbContext(optionsBuilder.Options, contextWrapper);
    }
}
