using System.Net;
using Gravity.Express.API.Models;
using Gravity.Express.Application.Common.Models;
using Gravity.Express.Application.Cqrs.Delivery.Commands.CreateDelivery;
using Gravity.Express.Application.Cqrs.Delivery.Queries.GetDeliveries;
using Gravity.Express.Application.Filter;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gravity.Express.API.Controllers;

public class DeliveriesController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public DeliveriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new delivery.
    /// </summary>
    /// <param name="request">The request object containing delivery information.</param>
    /// <returns>Returns the ID of the created delivery.</returns>
    // [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateDeliveryRequest request)
    {
        var createDeliveryCommandResponse = await _mediator.Send(new CreateDeliveryCommand()
        {
            CustomerId = request.CustomerId,
            DeliveryAddress = request.DeliveryAddress,
            senderWarehouseAddress = request.senderWarehouseAddress,
            TrackingNumber = request.TrackingNumber,
            IsPassive = request.IsPassive ?? false
        });

        return Ok(createDeliveryCommandResponse.Id);
    }

    /// <summary>
    /// Retrieves a delivery by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the delivery.</param>
    /// <returns>
    /// An <see cref="ActionResult{T}"/> containing the response for the delivery retrieval operation.
    /// If successful, returns HTTP 200 (OK) with the delivery information; otherwise, returns an error response.
    /// </returns>
    //[Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<GetDeliveriesQueryResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginatedList<GetDeliveriesQueryResponse>>> Get(
        [FromQuery] FilterModel filterModel)
    {
        return await _mediator.Send(new GetDeliveriesQuery(filterModel));
    }
}
