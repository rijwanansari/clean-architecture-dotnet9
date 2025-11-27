using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using MediatR;

namespace CleanArchitecture.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    AddressDto Address
) : IRequest<Result<Guid>>;
