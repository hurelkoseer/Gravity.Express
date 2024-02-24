using BinBin.Partnership.API.Filters;
using Gravity.Express.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<MultiTenantActionFilter>(0);
});

builder.Services.AddInfrastructure(builder.Configuration);

app.MapGet("/", () => "");

app.Run();
