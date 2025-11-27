using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using MediatR;

namespace CleanArchitecture.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    AddressDto Address
) : IRequest<Result<bool>>;
