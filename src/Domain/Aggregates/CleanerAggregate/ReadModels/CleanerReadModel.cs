using Domain.Abstractions;
using Domain.Aggregates.CleanerAggregate.ValueObjects;

namespace Domain.Aggregates.CleanerAggregate.ReadModels;

public class CleanerReadModel : EntityReadModel
{
    public required string Name { get; init; }
    public required string Description { get; init; }

    public List<CleanerOfferedService>? CleanerOfferedServices { get; init; }
}
