using Domain.Abstractions;
using Domain.Aggregates.ServiceOfferAggregate.ReadModels;

namespace Domain.Aggregates.CleanerAggregate.ReadModels;

public class CleanerReadModel : EntityReadModel
{
    public required Guid AggregateId { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }

    public List<ServiceOfferReadModel>? OfferedServices { get; init; }
    public List<ServiceAreaReadModel>? ServiceAreas { get; init; }
}
