using Domain.Abstractions;

namespace Domain.Aggregates.BookingAggregate.Entities;

public class BookingReview : Entity
{
    public Guid BookingAggregateId { get; private set; }
    public Guid ReviewAggregateId { get; private set; }
    public string Comment { get; private set; } = string.Empty;
    public int Rating { get; private set; }

    private BookingReview() { }

    public static BookingReview Create(
        Guid reviewAggregateId,
        Guid bookingAggregateId,
        string comment,
        int rating)
    {
        if (reviewAggregateId == Guid.Empty)
            throw new ArgumentException("Review aggregate ID cannot be empty", nameof(reviewAggregateId));
        if (bookingAggregateId == Guid.Empty)
            throw new ArgumentException("Booking aggregate ID cannot be empty", nameof(bookingAggregateId));
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment is required", nameof(comment));
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5", nameof(rating));

        return new BookingReview
        {
            ReviewAggregateId = reviewAggregateId,
            BookingAggregateId = bookingAggregateId,
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
