using Domain.Abstractions;

namespace Domain.Aggregates.BookingAggregate.Entities;

public class BookingReview : Entity
{
    public long BookingId { get; private set; }
    public Guid ReviewAggregateId { get; private set; }
    public string Comment { get; private set; } = string.Empty;
    public int Rating { get; private set; }

    private BookingReview() { }

    public static BookingReview Create(
        Guid reviewAggregateId,
        long bookingId,
        string comment,
        int rating)
    {
        if (reviewAggregateId == Guid.Empty)
            throw new ArgumentException("Review aggregate ID cannot be empty", nameof(reviewAggregateId));
        if (bookingId == 0)
            throw new ArgumentException("Booking aggregate ID cannot be empty", nameof(bookingId));
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment is required", nameof(comment));
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5", nameof(rating));

        return new BookingReview
        {
            ReviewAggregateId = reviewAggregateId,
            BookingId = bookingId,
            Comment = comment.Trim(),
            Rating = rating,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };
    }

    public void Update(string comment, int rating)
    {
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment is required", nameof(comment));
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5", nameof(rating));

        Comment = comment.Trim();
        Rating = rating;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
