namespace Domain.Abstractions;

public abstract class AggregateRoot : Entity
{
    public Guid AggregateId { get; protected set; }
    private readonly List<DomainEvent> _domainEvents = [];
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEvent domainEvent)
    {
        if (domainEvent == null)
            throw new ArgumentNullException(nameof(domainEvent));

        if (domainEvent.AggregateId == Guid.Empty)
            throw new InvalidOperationException("Domain event must have an AggregateId set");

        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void UpdateTimestamp()
    {
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
