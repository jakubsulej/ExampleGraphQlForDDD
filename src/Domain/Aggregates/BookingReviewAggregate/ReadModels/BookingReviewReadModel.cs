using Domain.Abstractions;
using Domain.Aggregates.BookingAggregate.ReadModels;

namespace Domain.Aggregates.BookingReviewAggregate.ReadModels;

public class BookingReviewReadModel : EntityReadModel
{
    public required Guid BookingAggregateId { get; init; }
    public required string Review { get; init; }
    public required int Rating { get; init; }

    public BookingReadModel? Booking { get; init; }
}
