using Domain.Abstractions;
using Domain.Aggregates.CleanerAggregate.ValueObjects;

namespace Domain.Aggregates.CleanerAggregate;

public class Cleaner : AggregateRoot
{
    public required string Name { get; set; }
    public required string Description { get; set; }

    private readonly List<CleanerOfferedService> _cleanerOfferedServices = [];
    public IReadOnlyCollection<CleanerOfferedService> CleanerOfferedServices => _cleanerOfferedServices.AsReadOnly();
}
