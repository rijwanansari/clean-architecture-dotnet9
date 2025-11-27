using CleanArchitecture.Application.Common;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands.DeactivateProduct;

public record DeactivateProductCommand(Guid ProductId) : IRequest<Result<bool>>;
