using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.DTOs.Common;
using CleanArchitecture.Application.DTOs.Customers;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDetailsDto>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<CustomerDetailsDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdWithOrdersAsync(request.Id, cancellationToken);
        if (customer == null)
            return Result<CustomerDetailsDto>.Failure($"Customer with ID {request.Id} not found");

        var dto = new CustomerDetailsDto(
            customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.Email.Value,
            customer.PhoneNumber,
            new AddressDto(
                customer.Address.Street,
                customer.Address.City,
                customer.Address.State,
                customer.Address.ZipCode,
                customer.Address.Country
            ),
            customer.Orders.Count,
            customer.CreatedAt,
            customer.UpdatedAt
        );

        return Result<CustomerDetailsDto>.Success(dto);
    }
}
