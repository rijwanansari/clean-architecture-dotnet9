using CleanArchitecture.Application.Abstractions.Caching;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CleanArchitecture.Infrastructure.HealthChecks;

/// <summary>
/// Health check for cache service availability
/// </summary>
public sealed class CacheHealthCheck : IHealthCheck
{
    private readonly ICacheService _cacheService;

    public CacheHealthCheck(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var testKey = $"health_check_{Guid.NewGuid()}";
            var testValue = DateTime.UtcNow.ToString();

            // Test set operation
            await _cacheService.SetAsync(testKey, testValue, TimeSpan.FromSeconds(10), cancellationToken);

            // Test get operation
            var retrievedValue = await _cacheService.GetAsync<string>(testKey, cancellationToken);

            // Test remove operation
            await _cacheService.RemoveAsync(testKey, cancellationToken);

            if (retrievedValue == testValue)
            {
                return HealthCheckResult.Healthy("Cache service is working properly");
            }

            return HealthCheckResult.Degraded("Cache service returned unexpected value");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                "Cache service is not working",
                exception: ex);
        }
    }
}
