using System;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<Product>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);
    Task<List<Product>> GetActiveProductsAsync(CancellationToken cancellationToken = default);
    Task<(List<Product> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, 
        CancellationToken cancellationToken = default);
    Task<(List<Product> Items, int TotalCount)> GetByCategoryPagedAsync(string category, int page, int pageSize,
        CancellationToken cancellationToken = default);
    Task<(List<Product> Items, int TotalCount)> SearchProductsAsync(string searchTerm, int page, int pageSize,
        CancellationToken cancellationToken = default);
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    void Update(Product product);
    void Delete(Product product);
}
