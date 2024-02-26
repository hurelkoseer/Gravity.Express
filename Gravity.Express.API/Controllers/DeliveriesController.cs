using System.Net;
using Gravity.Express.API.Models;
using Gravity.Express.Application.Cqrs.Delivery.Commands.CreateDelivery;
using Mediator;
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
}
