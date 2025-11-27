using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Products;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Products.Queries.GetProductsByCategory;

public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, Result<PagedResponse<ProductDto>>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByCategoryQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<PagedResponse<ProductDto>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var (products, totalCount) = await _productRepository.GetByCategoryPagedAsync(
            request.Category,
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
