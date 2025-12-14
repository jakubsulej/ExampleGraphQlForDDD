using Domain.Abstractions;
using Domain.Aggregates.ServiceOfferAggregate.ValueObjects;
using System.Collections.ObjectModel;

namespace Domain.Aggregates.ServiceOfferAggregate;

public class ServiceOffer : AggregateRoot
{
    public Guid CleanerAggregateId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }

    private readonly List<ServicePricing> _servicePricings = [];
    public ReadOnlyCollection<ServicePricing> ServicePricings => _servicePricings.AsReadOnly();
}
