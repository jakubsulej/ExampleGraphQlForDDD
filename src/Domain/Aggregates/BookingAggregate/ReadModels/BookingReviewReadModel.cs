using Domain.Abstractions;

namespace Domain.Aggregates.BookingAggregate.ReadModels;

public class BookingReviewReadModel : EntityReadModel
{
    public required int BookingId { get; init; }
    public required string Review { get; init; }
    public required int Rating { get; init; }

    public BookingReadModel? Booking { get; init; }
}
