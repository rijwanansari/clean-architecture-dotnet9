using FluentValidation;

namespace CleanArchitecture.Application.Products.Commands.UpdateProductStock;

public class UpdateProductStockCommandValidator : AbstractValidator<UpdateProductStockCommand>
{
    public UpdateProductStockCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(x => x.QuantityChange)
            .NotEqual(0)
            .WithMessage("Quantity change cannot be zero")
            .GreaterThan(-10000)
            .WithMessage("Quantity change cannot be less than -10,000")
            .LessThan(10000)
            .WithMessage("Quantity change cannot exceed 10,000");
    }
}
