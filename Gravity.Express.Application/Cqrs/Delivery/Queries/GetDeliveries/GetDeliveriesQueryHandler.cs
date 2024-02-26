using Gravity.Express.Application.Common.Models;
using Gravity.Express.Application.Filter;
using Gravity.Express.Application.Mapping;
using Gravity.Express.Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Gravity.Express.Application.Cqrs.Delivery.Queries.GetDeliveries;

public class GetDeliveriesQueryHandler : IQueryHandler<GetDeliveriesQuery, PaginatedList<GetDeliveriesQueryResponse>>
{
    private readonly IAppDbContext _appDbContext;

    private readonly IFilterProcessor _filterProcessor;


    public GetDeliveriesQueryHandler(IAppDbContext appDbContext, IFilterProcessor filterProcessor)
    {
        _appDbContext = appDbContext;
        _filterProcessor = filterProcessor;
    }

    public async ValueTask<PaginatedList<GetDeliveriesQueryResponse>> Handle(GetDeliveriesQuery request,
                                                                             CancellationToken cancellationToken)
    {
        var transactions =
            _appDbContext.Deliveries
                         .AsNoTracking();

        return await _filterProcessor
                     .Apply(request.Filter, transactions, applyPagination: false)
                     .Select(q => new GetDeliveriesQueryResponse(q.Id, q.DeliveryAddress, q.SenderWarehouseAddress,
                                                                 q.TrackingNumber,
                                                                 q.TenantId
                                                                 , q.IsPassive))
                     .PaginatedListAsync(request.Filter.Page!.Value, request.Filter.PageSize!.Value);
    }
}
