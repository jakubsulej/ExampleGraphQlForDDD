using Domain.Abstractions;
using Domain.Aggregates.BookingAggregate.ValueObjects;
using System.Collections.ObjectModel;

namespace Domain.Aggregates.BookingAggregate;

public class Booking : AggregateRoot
{
    public Guid ServiceOfferAggregateId { get; set; }
    public Guid CustomerAggregateId { get; set; }

    public readonly List<ServicePricingSnapshot> _servicePricingSnapshots = [];
    public ReadOnlyCollection<ServicePricingSnapshot> ServicePricingSnapshots => _servicePricingSnapshots.AsReadOnly();
}
