using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Products;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Products.Queries.SearchProducts;

public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, Result<PagedResponse<ProductDto>>>
{
    private readonly IProductRepository _productRepository;

    public SearchProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<PagedResponse<ProductDto>>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.SearchTerm))
            return Result<PagedResponse<ProductDto>>.Success(
                new PagedResponse<ProductDto>(new List<ProductDto>(), 0, request.Page, request.PageSize));

        var (products, totalCount) = await _productRepository.SearchProductsAsync(
            request.SearchTerm,
            request.Page,
            request.PageSize,
            cancellationToken
        );

        var productDtos = products.Select(p => new ProductDto(
            p.Id,
            p.Name,
            p.Description,
            p.Price.Amount,
            p.Price.Currency,
            p.StockQuantity,
            p.Category,
            p.IsActive,
            p.CreatedAt
        )).ToList();

        var response = new PagedResponse<ProductDto>(productDtos, totalCount, request.Page, request.PageSize);
        return Result<PagedResponse<ProductDto>>.Success(response);
    }
}
