using CleanArchitecture.Application.Common;
using MediatR;

namespace CleanArchitecture.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(Guid Id) : IRequest<Result<bool>>;
