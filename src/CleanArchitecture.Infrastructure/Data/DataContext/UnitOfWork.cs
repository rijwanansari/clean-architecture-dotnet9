using System;
using CleanArchitecture.Application.Abstractions.Data;

namespace CleanArchitecture.Infrastructure.Data.DataContext;

public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _context;

    public UnitOfWork(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
