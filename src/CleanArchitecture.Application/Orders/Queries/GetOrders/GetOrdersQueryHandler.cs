using System;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Orders;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<PagedResponse<OrderDto>>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<PagedResponse<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var (orders, totalCount) = await _orderRepository.GetPagedAsync(
            request.Page,
            request.PageSize,
            cancellationToken
        );

        var orderDtos = orders.Select(order => new OrderDto(
            order.Id,
            order.OrderNumber,
            order.CustomerId,
            order.Customer?.GetFullName() ?? string.Empty,
            order.Status,
            order.GetTotalAmount().Amount,
            order.GetTotalAmount().Currency,
            order.CreatedAt
        )).ToList();

        var response = new PagedResponse<OrderDto>(orderDtos, totalCount, request.Page, request.PageSize);
        return Result<PagedResponse<OrderDto>>.Success(response);
    }

}
