using System;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IApplicationDbContext _context;

    public ProductRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => p.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => p.Category == category && p.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<(List<Product> Items, int TotalCount)> GetPagedAsync(
        int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.Where(p => p.IsActive);
        
        var totalCount = await query.CountAsync(cancellationToken);
        
        var items = await query
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }

}
