using FluentValidation;

namespace CleanArchitecture.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum()
            .WithMessage("Invalid payment method");

        RuleFor(x => x.ShippingAddress)
            .NotNull()
            .WithMessage("Shipping address is required");

        When(x => x.ShippingAddress != null, () =>
        {
            RuleFor(x => x.ShippingAddress.Street)
                .NotEmpty()
                .WithMessage("Street address is required")
                .MaximumLength(200)
                .WithMessage("Street address cannot exceed 200 characters");

            RuleFor(x => x.ShippingAddress.City)
                .NotEmpty()
                .WithMessage("City is required")
                .MaximumLength(100)
                .WithMessage("City cannot exceed 100 characters");

            RuleFor(x => x.ShippingAddress.State)
                .NotEmpty()
                .WithMessage("State is required")
                .MaximumLength(100)
                .WithMessage("State cannot exceed 100 characters");

            RuleFor(x => x.ShippingAddress.ZipCode)
                .NotEmpty()
                .WithMessage("Zip code is required")
                .Matches(@"^\d{5}(-\d{4})?$")
                .WithMessage("Invalid zip code format");

            RuleFor(x => x.ShippingAddress.Country)
                .NotEmpty()
                .WithMessage("Country is required")
                .MaximumLength(100)
                .WithMessage("Country cannot exceed 100 characters");
        });

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Order must contain at least one item");

        RuleForEach(x => x.Items)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId)
                    .NotEmpty()
                    .WithMessage("Product ID is required");

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than zero")
                    .LessThanOrEqualTo(1000)
                    .WithMessage("Quantity cannot exceed 1000 per item");
            });
    }
}
