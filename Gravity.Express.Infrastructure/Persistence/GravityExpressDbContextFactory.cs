using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gravity.Express.Infrastructure.Persistence;

public class GravityExpressDbContextFactory : IDesignTimeDbContextFactory<GravityExpressDbContext>
{
    public GravityExpressDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GravityExpressDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=GravityExpress;Username=gravity;Password=express");

        var contextWrapper = new NullContextWrapper();

        return new GravityExpressDbContext(optionsBuilder.Options, contextWrapper);
    }
}
