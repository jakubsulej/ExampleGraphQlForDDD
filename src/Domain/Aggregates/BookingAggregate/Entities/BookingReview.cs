using Domain.Abstractions;

namespace Domain.Aggregates.BookingAggregate.Entities;

public class BookingReview : Entity
{
    public int BookingId { get; set; }

}
