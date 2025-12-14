using Domain.Abstractions;

namespace Domain.Aggregates.CleanerAggregate.ValueObjects;

public class CleanerOfferedService : ValueObject
{
    public Guid OfferedServiceAggregateId { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return OfferedServiceAggregateId;
    }
}
