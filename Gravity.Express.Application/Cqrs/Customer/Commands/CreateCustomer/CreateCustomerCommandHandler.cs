using Gravity.Express.Application.Exceptions;
using Gravity.Express.Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MassTransit;
using Gravity.Express.Domain.Entities;

namespace Gravity.Express.Application.Cqrs.Customer.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
{
    private readonly IAppDbContext _appDbContext;

    public CreateCustomerCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async ValueTask<CreateCustomerCommandResponse> Handle(CreateCustomerCommand request,
                                                                 CancellationToken cancellationToken)
    {
        var isExistsCustomer =
            await _appDbContext.Customers.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);

        if (isExistsCustomer)
        {
            throw new ValidationFailedException("The user with this Name already exists.");
        }

        var customer = new Domain.Entities.Customer { Name = request.Name, IsPassive = request.IsPassive };

        _appDbContext.Customers.Add(customer);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return new CreateCustomerCommandResponse(customer.Id);
    }
}
