using System;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Customer?> GetByIdWithOrdersAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<(List<Customer> Items, int TotalCount)> GetPagedAsync(int page, int pageSize,
        CancellationToken cancellationToken = default);
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    void Update(Customer customer);
    void Delete(Customer customer);
}
