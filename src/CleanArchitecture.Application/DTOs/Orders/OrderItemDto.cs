namespace CleanArchitecture.Application.DTOs.Orders;

/// <summary>
/// Order item DTO for creating orders
/// </summary>
public record OrderItemDto(
    Guid ProductId,
    int Quantity
);
