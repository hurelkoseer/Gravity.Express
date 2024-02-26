using FluentValidation;

namespace Gravity.Express.Application.Cqrs.Delivery.Commands.CreateDelivery;

public class CreateDeliveryCommandValidator : AbstractValidator<CreateDeliveryCommand>
{
    public CreateDeliveryCommandValidator()
    {
        RuleFor(q => q.senderWarehouseAddress)
            .NotEmpty()
            .WithMessage("Sender Warehouse Address is required.");

        RuleFor(q => q.DeliveryAddress)
            .NotEmpty()
            .WithMessage("Delivery Address is required.");

        RuleFor(q => q.CustomerId)
            .NotEmpty()
            .WithMessage("Customer Id is required.");

        RuleFor(q => q.TrackingNumber)
            .NotEmpty()
            .WithMessage("Tracking Number is required.");

    }
}
