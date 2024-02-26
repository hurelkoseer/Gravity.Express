using Mediator;

namespace Gravity.Express.Application.Cqrs.Customer.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<CreateCustomerCommandResponse>
{
    public required string Name { get; set; }

    public required bool IsPassive { get; set; }
}
