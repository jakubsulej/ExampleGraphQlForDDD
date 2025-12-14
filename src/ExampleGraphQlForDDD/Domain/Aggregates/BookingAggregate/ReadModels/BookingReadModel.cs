namespace Domain.Aggregates.BookingAggregate.ReadModels;

public class BookingReadModel
{
    public Guid ServiceOfferAggregateId { get; set; }
    public Guid CustomerAggregateId { get; set; }
    public List<ServicePricingSnapshotReadModel> ServicePricingSnapshots { get; set; } = [];
}
