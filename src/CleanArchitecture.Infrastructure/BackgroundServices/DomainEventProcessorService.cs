using CleanArchitecture.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.BackgroundServices;

/// <summary>
/// Background service for processing domain events that might have failed or need retry
/// </summary>
public sealed class DomainEventProcessorService : BackgroundService
{
    private readonly ILogger<DomainEventProcessorService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _processingInterval = TimeSpan.FromMinutes(5);

    public DomainEventProcessorService(
        ILogger<DomainEventProcessorService> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Domain Event Processor Service is starting");

        // Delay startup to allow application to fully initialize
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogDebug("Domain Event Processor Service is working at: {Time}", DateTimeOffset.UtcNow);

                using (var scope = _serviceProvider.CreateScope())
                {
                    // In a real implementation with Outbox pattern:
                    // 1. Query for unprocessed domain events from outbox table
                    // 2. Publish them to the message bus
                    // 3. Mark them as processed
                    
                    // For now, just log that the service is running
                    _logger.LogDebug("Domain event processing cycle completed");
                }

                await Task.Delay(_processingInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Domain Event Processor Service is stopping");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing Domain Event Processor Service");
                
                // Wait before retrying to avoid rapid failure loops
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        _logger.LogInformation("Domain Event Processor Service has stopped");
    }
}
