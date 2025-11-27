using CleanArchitecture.Application.DTOs.Common;

namespace CleanArchitecture.Application.DTOs.Products;

/// <summary>
/// Detailed product DTO
/// </summary>
public record ProductDetailsDto(
    Guid Id,
    string Name,
    string Description,
    MoneyDto Price,
    int StockQuantity,
    string Category,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
