namespace CleanArchitecture.Application.DTOs.Products;

/// <summary>
/// Product list item DTO
/// </summary>
public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    int StockQuantity,
    string Category,
    bool IsActive,
    DateTime CreatedAt
);
