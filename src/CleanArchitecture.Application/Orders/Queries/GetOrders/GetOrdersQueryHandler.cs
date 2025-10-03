using System;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<OrderListResponse>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderListResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
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
            order.Customer.GetFullName(),
            order.Status,
            order.GetTotalAmount().Amount,
            order.CreatedAt
        )).ToList();

        var response = new OrderListResponse(orderDtos, totalCount, request.Page, request.PageSize);
        return Result<OrderListResponse>.Success(response);
    }

}
