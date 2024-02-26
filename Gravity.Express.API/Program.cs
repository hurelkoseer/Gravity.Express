using Finbuckle.MultiTenant;
using Gravity.Express.API.Filters;
using Gravity.Express.API.Services;
using Gravity.Express.Application;
using Gravity.Express.Application.Cqrs.Delivery.Queries.GetDeliveries;
using Gravity.Express.Application.Filter;
using Gravity.Express.BackgroundTasks.Extensions;
using Gravity.Express.Infrastructure;
using Gravity.Express.Infrastructure.Persistence;
using Sieve.Models;
using Sieve.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<MultiTenantActionFilter>(0);
});

builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddOptions();
builder.Services.AddMultiTenant<TenantInfo>()
       .WithConfigurationStore()
       .WithRouteStrategy("tenant");
builder.Services.AddCustomMassTransit(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IContextWrapper, HttpContextWrapper>();
builder.Services.AddScoped<MultiTenantActionFilter>();
builder.Services.Configure<SieveOptions>(builder.Configuration.GetSection("FilterOptions"));
builder.Services.AddScoped<ISieveCustomSortMethods, GetDeliveriesQuerySortings>();
builder.Services.AddScoped<ISieveCustomFilterMethods, GetDeliveriesQueryFilters>();
builder.Services.AddScoped<IFilterProcessor, FilterProcessor>();

var app = builder.Build();
app.UseRouting();app.UseMultiTenant();
app.UseAuthentication();
app.UseAuthorization();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gravity Express");
    });
}

app.MapControllers();
app.Run();
