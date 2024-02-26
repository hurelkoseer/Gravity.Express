using System.Net;
using Gravity.Express.API.Models;
using Gravity.Express.Application.Cqrs.Customer.Commands.CreateCustomer;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Gravity.Express.API.Controllers;

public class CustomersController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="request">The request object containing customer information.</param>
    /// <returns>Returns the ID of the created customer.</returns>
    // [Authorize(Operations.CreateScopeName)]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateCustomerRequest request)
    {
        var createCustomerCommandResponse = await _mediator.Send(new CreateCustomerCommand()
        {
            Name = request.Name,
            IsPassive = request.IsPassive ?? false
        });

        return Ok(createCustomerCommandResponse.Id);
    }
}
