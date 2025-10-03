using System;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Enumerators;
using MediatR;

namespace CleanArchitecture.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    PaymentMethod PaymentMethod,
    ShippingAddressDto ShippingAddress,
    List<OrderItemDto> Items
) : IRequest<Result<Guid>>;

public record ShippingAddressDto(
    string Street,
    string City,
    string State,
    string ZipCode,
    string Country
);

public record OrderItemDto(Guid ProductId, int Quantity);
