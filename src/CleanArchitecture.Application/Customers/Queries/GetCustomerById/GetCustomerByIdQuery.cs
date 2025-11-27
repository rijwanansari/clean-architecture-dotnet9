using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Customers;
using MediatR;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(Guid Id) : IRequest<Result<CustomerDetailsDto>>;
