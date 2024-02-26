using FluentValidation;

namespace Gravity.Express.Application.Cqrs.Customer.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty()
            .WithMessage("Customer name is required.");
    }
}
