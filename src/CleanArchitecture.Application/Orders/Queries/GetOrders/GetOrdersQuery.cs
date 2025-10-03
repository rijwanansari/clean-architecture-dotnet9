using System;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Enumerators;
using MediatR;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(int Page = 1, int PageSize = 10) : IRequest<Result<OrderListResponse>>;

public record OrderListResponse(List<OrderDto> Orders, int TotalCount, int Page, int PageSize);

public record OrderDto(
    Guid Id,
    string OrderNumber,
    Guid CustomerId,
    string CustomerName,
    OrderStatus Status,
    decimal TotalAmount,
    DateTime CreatedAt
);
