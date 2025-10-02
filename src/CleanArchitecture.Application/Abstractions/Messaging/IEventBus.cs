using System;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Abstractions.Messaging;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : IDomainEvent;
}
