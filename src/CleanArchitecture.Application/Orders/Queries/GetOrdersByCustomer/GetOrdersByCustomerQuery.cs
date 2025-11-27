using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Orders;
using MediatR;

namespace CleanArchitecture.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(
    Guid CustomerId,
    int Page = 1,
    int PageSize = 10
) : IRequest<Result<PagedResponse<OrderDto>>>;
