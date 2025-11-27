using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.BackgroundServices;

/// <summary>
/// Background service for cleaning up expired cache entries periodically
/// </summary>
public sealed class CacheCleanupService : BackgroundService
{
    private readonly ILogger<CacheCleanupService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _cleanupInterval = TimeSpan.FromHours(1);

    public CacheCleanupService(
        ILogger<CacheCleanupService> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Cache Cleanup Service is starting");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Cache Cleanup Service is working at: {Time}", DateTimeOffset.UtcNow);

                using (var scope = _serviceProvider.CreateScope())
                {
                    // Perform cleanup logic here
                    // For in-memory cache, this is handled automatically
                    // For distributed cache (Redis/SQL), implement cleanup logic
                    
                    _logger.LogInformation("Cache cleanup completed successfully");
                }

                await Task.Delay(_cleanupInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Service is stopping
                _logger.LogInformation("Cache Cleanup Service is stopping");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing Cache Cleanup Service");
                
                // Wait before retrying to avoid rapid failure loops
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        _logger.LogInformation("Cache Cleanup Service has stopped");
    }
}
