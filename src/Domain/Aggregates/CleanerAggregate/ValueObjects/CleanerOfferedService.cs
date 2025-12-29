using Domain.Abstractions;

namespace Domain.Aggregates.CleanerAggregate.ValueObjects;

public class CleanerOfferedService : ValueObject
{
    public Guid OfferedServiceAggregateId { get; private init; }

    // Private constructor for EF Core
    private CleanerOfferedService() { }

    // Factory method for creating value objects
    public CleanerOfferedService(Guid offeredServiceAggregateId)
    {
        if (offeredServiceAggregateId == Guid.Empty)
            throw new ArgumentException("Offered service aggregate ID cannot be empty", nameof(offeredServiceAggregateId));

        OfferedServiceAggregateId = offeredServiceAggregateId;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return OfferedServiceAggregateId;
    }
}
