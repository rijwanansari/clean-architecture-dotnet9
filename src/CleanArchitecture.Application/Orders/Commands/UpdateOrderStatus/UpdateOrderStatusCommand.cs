using System;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Enumerators;
using MediatR;

namespace CleanArchitecture.Application.Orders.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(Guid OrderId, OrderStatus NewStatus) : IRequest<Result<bool>>;

