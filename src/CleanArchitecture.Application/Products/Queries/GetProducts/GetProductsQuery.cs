using System;
using CleanArchitecture.Application.Common;
using MediatR;

namespace CleanArchitecture.Application.Products.Queries.GetProducts;

public record GetProductsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<ProductListResponse>>;

public record ProductListResponse(List<ProductDto> Products, int TotalCount, int Page, int PageSize);

public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    int StockQuantity,
    string Category,
    bool IsActive
);
