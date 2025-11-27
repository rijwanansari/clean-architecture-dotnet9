using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Customers;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, Result<PagedResponse<CustomerDto>>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<PagedResponse<CustomerDto>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var (customers, totalCount) = await _customerRepository.GetPagedAsync(
            request.Page,
            request.PageSize,
            cancellationToken
        );

        var customerDtos = customers.Select(c => new CustomerDto(
            c.Id,
            c.FirstName,
            c.LastName,
            c.GetFullName(),
            c.Email.Value,
            c.PhoneNumber,
            c.CreatedAt
        )).ToList();

        var response = new PagedResponse<CustomerDto>(customerDtos, totalCount, request.Page, request.PageSize);
        return Result<PagedResponse<CustomerDto>>.Success(response);
    }
}
