using Domain.Abstractions;
using Domain.Aggregates.BookingReviewAggregate.ReadModels;
using Domain.Aggregates.CustomerAggregate.ReadModels;
using Domain.Aggregates.ServiceOfferAggregate.ReadModels;

namespace Domain.Aggregates.BookingAggregate.ReadModels;

public class BookingReadModel : EntityReadModel
{
    public required Guid ServiceOfferAggregateId { get; init; }
    public required Guid CustomerAggregateId { get; init; }

    public ServiceOfferReadModel? ServiceOffer { get; init; }
    public CustomerReadModel? Customer { get; init; }
    public List<ServicePricingSnapshotReadModel>? ServicePricingSnapshots { get; init; }
    public List<BookingReviewReadModel>? BookingReviews { get; init; }
}
