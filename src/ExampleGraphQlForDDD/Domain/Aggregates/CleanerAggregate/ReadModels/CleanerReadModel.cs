using Domain.Aggregates.CleanerAggregate.ValueObjects;

namespace Domain.Aggregates.CleanerAggregate.ReadModels;

public class CleanerReadModel
{
    public required string Name { get; set; }
    public required string Description { get; set; }

    private readonly List<CleanerOfferedService> _cleanerOfferedServices = [];
}
