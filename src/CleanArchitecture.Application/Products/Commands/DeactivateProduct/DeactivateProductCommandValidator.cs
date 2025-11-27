using FluentValidation;

namespace CleanArchitecture.Application.Products.Commands.DeactivateProduct;

public class DeactivateProductCommandValidator : AbstractValidator<DeactivateProductCommand>
{
    public DeactivateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
