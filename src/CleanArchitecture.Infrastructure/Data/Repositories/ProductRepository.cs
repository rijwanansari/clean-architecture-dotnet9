using System;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
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

    public async Task<List<Product>> GetActiveProductsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
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
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page), "Page must be greater than or equal to 1.");
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");

        var query = _context.Products.Where(p => p.IsActive);
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<(List<Product> Items, int TotalCount)> GetByCategoryPagedAsync(
        string category, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category is required", nameof(category));

        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page), "Page must be greater than or equal to 1.");
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");

        var query = _context.Products.Where(p => p.IsActive && p.Category == category);
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<(List<Product> Items, int TotalCount)> SearchProductsAsync(
        string searchTerm, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetPagedAsync(page, pageSize, cancellationToken);

        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page), "Page must be greater than or equal to 1.");
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");

        var normalized = searchTerm.Trim().ToLowerInvariant();
        var query = _context.Products.Where(p => p.IsActive &&
            (EF.Functions.Like(p.Name.ToLower(), "%" + normalized + "%") ||
             EF.Functions.Like(p.Description.ToLower(), "%" + normalized + "%") ||
             EF.Functions.Like(p.Category.ToLower(), "%" + normalized + "%")));

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