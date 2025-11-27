using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArchitecture.Infrastructure.Data.Interceptors;

/// <summary>
/// Interceptor that dispatches domain events after a successful save operation
/// </summary>
public sealed class DomainEventDispatcherInterceptor : SaveChangesInterceptor
{
    private readonly IEventBus _eventBus;

    public DomainEventDispatcherInterceptor(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            await DispatchDomainEventsAsync(eventData.Context, cancellationToken);
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public override int SavedChanges(
        SaveChangesCompletedEventData eventData,
        int result)
    {
        if (eventData.Context is not null)
        {
            DispatchDomainEventsAsync(eventData.Context).GetAwaiter().GetResult();
        }

        return base.SavedChanges(eventData, result);
    }

    private async Task DispatchDomainEventsAsync(DbContext context, CancellationToken cancellationToken = default)
    {
        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        // Clear events before dispatching to prevent infinite loops
        entities.ForEach(e => e.ClearDomainEvents());

        // Dispatch each event
        foreach (var domainEvent in domainEvents)
        {
            await _eventBus.PublishAsync(domainEvent, cancellationToken);
        }
    }
}
