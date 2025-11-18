using System;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Common;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Services;

public class EventBus : IEventBus
{
    private readonly ILogger<EventBus> _logger;

    public EventBus(ILogger<EventBus> logger)
    {
        _logger = logger;
    }

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : IDomainEvent
    {
        _logger.LogInformation("Publishing event: {EventType} - {EventId}", 
            @event.GetType().Name, @event.EventId);
        
        // In production, this would publish to a message broker (RabbitMQ, Azure Service Bus, etc.)
        return Task.CompletedTask;
    }
}
