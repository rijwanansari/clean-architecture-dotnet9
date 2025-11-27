using FluentValidation;

namespace CleanArchitecture.Application.Products.Queries.SearchProducts;

public class SearchProductsQueryValidator : AbstractValidator<SearchProductsQuery>
{
    public SearchProductsQueryValidator()
    {
        RuleFor(x => x.SearchTerm)
            .NotEmpty()
            .WithMessage("Search term is required")
            .MinimumLength(2)
            .WithMessage("Search term must be at least 2 characters")
            .MaximumLength(200)
            .WithMessage("Search term cannot exceed 200 characters");

        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0")
            .LessThanOrEqualTo(100)
            .WithMessage("Page size cannot exceed 100");
    }
}
