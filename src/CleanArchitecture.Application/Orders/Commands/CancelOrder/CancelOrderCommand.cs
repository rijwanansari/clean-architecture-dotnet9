using CleanArchitecture.Application.Common;
using MediatR;

namespace CleanArchitecture.Application.Orders.Commands.CancelOrder;

public record CancelOrderCommand(
    Guid OrderId,
    string? Reason
) : IRequest<Result<bool>>;
