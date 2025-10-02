using System;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.DomainEvents;

public record OrderCreatedEvent(Guid OrderId, string OrderNumber) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
