using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;

namespace CleanArchitecture.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<Guid>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Check if customer with email already exists
            var existingCustomer = await _customerRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingCustomer != null)
                return Result<Guid>.Failure($"Customer with email {request.Email} already exists");

            var email = Email.Create(request.Email);
            var address = Address.Create(
                request.Address.Street,
                request.Address.City,
                request.Address.State,
                request.Address.ZipCode,
                request.Address.Country
            );

            var customer = Customer.Create(
                request.FirstName,
                request.LastName,
                email,
                request.PhoneNumber,
                address
            );

            await _customerRepository.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(customer.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure(ex.Message);
        }
    }
}
