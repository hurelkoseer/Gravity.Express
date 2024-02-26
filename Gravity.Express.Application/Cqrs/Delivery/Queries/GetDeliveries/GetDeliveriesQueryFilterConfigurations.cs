using Gravity.Express.Application.Filter;
using Sieve.Services;

namespace Gravity.Express.Application.Cqrs.Delivery.Queries.GetDeliveries;

public class GetDeliveriesQueryFilterConfigurations : IFilterConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Domain.Entities.Delivery>(x => x.Id)
              .CanFilter()
              .CanSort();

        mapper.Property<Domain.Entities.Delivery>(x => x.TrackingNumber)
              .CanFilter()
              .CanSort();

        mapper.Property<Domain.Entities.Delivery>(x => x.DeliveryAddress)
              .CanFilter()
              .CanSort();

        mapper.Property<Domain.Entities.Delivery>(x => x.TenantId)
              .CanFilter()
              .CanSort();

        mapper.Property<Domain.Entities.Delivery>(x => x.IsPassive)
              .CanFilter()
              .CanSort();
    }
}
