using Domain.Abstractions;

namespace Domain.Aggregates.BookingReviewAggregate;

public class BookingReview : AggregateRoot
{
    public Guid BookingAggregateId { get; set; }
    public required string Review { get; set; }
    public int Rating { get; set; }
}
