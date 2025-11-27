using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Products;
using MediatR;

namespace CleanArchitecture.Application.Products.Queries.GetProductsByCategory;

public record GetProductsByCategoryQuery(
    string Category,
    int Page = 1,
    int PageSize = 10
) : IRequest<Result<PagedResponse<ProductDto>>>;
