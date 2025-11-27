using CleanArchitecture.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CleanArchitecture.Infrastructure.HealthChecks;

/// <summary>
/// Health check for database connectivity
/// </summary>
public sealed class DatabaseHealthCheck : IHealthCheck
{
    private readonly IApplicationDbContext _context;

    public DatabaseHealthCheck(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Try to execute a simple query to check database connectivity
            await _context.Products.Take(1).ToListAsync(cancellationToken);

            return HealthCheckResult.Healthy("Database is accessible");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                "Database is not accessible",
                exception: ex);
        }
    }
}
