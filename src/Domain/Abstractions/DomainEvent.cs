using MediatR;

namespace Domain.Abstractions;

public abstract class DomainEvent : INotification
{
    public Guid AggregateId { get; init; }
    public DateTimeOffset Timestamp { get; init; }
}
