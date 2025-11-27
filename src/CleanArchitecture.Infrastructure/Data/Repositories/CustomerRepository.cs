using System;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
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

    public async Task<Customer?> GetByIdWithOrdersAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.ToLowerInvariant();
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.Email.Value == normalizedEmail, cancellationToken);
    }

    public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Customers.ToListAsync(cancellationToken);
    }

    public async Task<(List<Customer> Items, int TotalCount)> GetPagedAsync(
        int page, int pageSize, CancellationToken cancellationToken = default)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page), "Page must be greater than or equal to 1.");
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");

        var query = _context.Customers.AsQueryable();
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(c => c.LastName).ThenBy(c => c.FirstName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return (items, totalCount);
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _context.Customers.AddAsync(customer, cancellationToken);
    }

    public void Update(Customer customer)
    {
        _context.Customers.Update(customer);
    }

    public void Delete(Customer customer)
    {
        _context.Customers.Remove(customer);
    }

}
