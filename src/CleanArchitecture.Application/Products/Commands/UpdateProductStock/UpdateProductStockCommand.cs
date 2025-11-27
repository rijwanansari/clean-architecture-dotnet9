using CleanArchitecture.Application.Common;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands.UpdateProductStock;

public record UpdateProductStockCommand(
    Guid ProductId,
    int QuantityChange
) : IRequest<Result<bool>>;
