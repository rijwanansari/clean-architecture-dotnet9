using CleanArchitecture.Domain.Enumerators;

namespace CleanArchitecture.Application.DTOs.Orders;

/// <summary>
/// Order list item DTO
/// </summary>
public record OrderDto(
    Guid Id,
    string OrderNumber,
    Guid CustomerId,
    string CustomerName,
    OrderStatus Status,
    decimal TotalAmount,
    string Currency,
    DateTime CreatedAt
);
