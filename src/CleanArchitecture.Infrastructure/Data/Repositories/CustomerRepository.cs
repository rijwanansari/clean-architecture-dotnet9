using System;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IApplicationDbContext _context;

    public CustomerRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.Email.Value == email.ToLowerInvariant(), cancellationToken);
    }

    public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Customers.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _context.Customers.AddAsync(customer, cancellationToken);
    }

    public void Update(Customer customer)
    {
        _context.Customers.Update(customer);
    }

}
