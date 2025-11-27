using System;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Orders;
using CleanArchitecture.Domain.Enumerators;
using MediatR;

namespace CleanArchitecture.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    PaymentMethod PaymentMethod,
    AddressDto ShippingAddress,
    List<OrderItemDto> Items
) : IRequest<Result<Guid>>;
