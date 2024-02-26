using Gravity.Express.Application.Common.Models;
using Gravity.Express.Application.Filter;
using Mediator;

namespace Gravity.Express.Application.Cqrs.Delivery.Queries.GetDeliveries;

public class GetDeliveriesQuery : IQuery<PaginatedList<GetDeliveriesQueryResponse>>
{
    public GetDeliveriesQuery(FilterModel filterModel) => Filter = filterModel;

    public FilterModel Filter { get; }
}
