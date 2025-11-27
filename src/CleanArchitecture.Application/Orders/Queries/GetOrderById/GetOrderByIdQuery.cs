using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Orders;
using MediatR;

namespace CleanArchitecture.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IRequest<Result<OrderDetailsDto>>;
