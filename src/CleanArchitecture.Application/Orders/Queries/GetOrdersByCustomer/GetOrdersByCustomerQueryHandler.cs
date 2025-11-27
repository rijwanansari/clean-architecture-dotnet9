using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Orders;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerQueryHandler : IRequestHandler<GetOrdersByCustomerQuery, Result<PagedResponse<OrderDto>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    public GetOrdersByCustomerQueryHandler(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Result<PagedResponse<OrderDto>>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer == null)
            return Result<PagedResponse<OrderDto>>.Failure($"Customer with ID {request.CustomerId} not found");

        var (orders, totalCount) = await _orderRepository.GetByCustomerPagedAsync(
            request.CustomerId,
            request.Page,
            request.PageSize,
            cancellationToken
        );

        var orderDtos = orders.Select(order => new OrderDto(
            order.Id,
            order.OrderNumber,
            order.CustomerId,
            customer.GetFullName(),
            order.Status,
            order.GetTotalAmount().Amount,
            order.GetTotalAmount().Currency,
            order.CreatedAt
        )).ToList();

        var response = new PagedResponse<OrderDto>(orderDtos, totalCount, request.Page, request.PageSize);
        return Result<PagedResponse<OrderDto>>.Success(response);
    }
}
