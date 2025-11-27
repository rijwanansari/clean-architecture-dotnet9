using FluentValidation;

namespace CleanArchitecture.Application.Products.Commands.ActivateProduct;

public class ActivateProductCommandValidator : AbstractValidator<ActivateProductCommand>
{
    public ActivateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
