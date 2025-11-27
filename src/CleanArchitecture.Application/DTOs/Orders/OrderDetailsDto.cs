using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Domain.Enumerators;

namespace CleanArchitecture.Application.DTOs.Orders;

/// <summary>
/// Detailed order DTO with all information
/// </summary>
public record OrderDetailsDto(
    Guid Id,
    string OrderNumber,
    Guid CustomerId,
    string CustomerName,
    string CustomerEmail,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    AddressDto ShippingAddress,
    List<OrderItemDetailsDto> Items,
    MoneyDto Total,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

/// <summary>
/// Order item with product details
/// </summary>
public record OrderItemDetailsDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    MoneyDto UnitPrice,
    MoneyDto TotalPrice
);
