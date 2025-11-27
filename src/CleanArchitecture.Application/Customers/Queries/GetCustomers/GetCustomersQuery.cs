using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Customers;
using MediatR;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomers;

public record GetCustomersQuery(int Page = 1, int PageSize = 10) : IRequest<Result<PagedResponse<CustomerDto>>>;
