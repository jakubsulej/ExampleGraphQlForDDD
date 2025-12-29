using Domain.Abstractions;
using Domain.Aggregates.CleanerAggregate.ReadModels;

namespace Domain.Aggregates.ServiceOfferAggregate.ReadModels;

public class ServiceOfferReadModel : EntityReadModel
{
    public required Guid AggregateId { get; init; }
    public required Guid CleanerAggregateId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }

    public CleanerReadModel? Cleaner { get; init; }
    public List<ServicePricingReadModel>? ServicePricings {  get; init; }
}
