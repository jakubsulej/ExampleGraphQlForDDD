using Domain.Abstractions;

namespace Domain.Aggregates.ReviewAggregate;

public class Review : AggregateRoot
{
    public Guid BookingAggregateId { get; private set; }
    public Guid CustomerAggregateId { get; private set; }
    public string Comment { get; private set; } = string.Empty;
    public int Rating { get; private set; }
    public bool IsPublished { get; private set; }
    public DateTimeOffset? PublishedAt { get; private set; }

    private Review() { }

    public static Review Create(
        Guid aggregateId,
        Guid bookingAggregateId,
        Guid customerAggregateId,
        string comment,
        int rating)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentException("Aggregate ID cannot be empty", nameof(aggregateId));
        if (bookingAggregateId == Guid.Empty)
            throw new ArgumentException("Booking aggregate ID cannot be empty", nameof(bookingAggregateId));
        if (customerAggregateId == Guid.Empty)
            throw new ArgumentException("Customer aggregate ID cannot be empty", nameof(customerAggregateId));
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment is required", nameof(comment));
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5", nameof(rating));

        var review = new Review
        {
            AggregateId = aggregateId,
            BookingAggregateId = bookingAggregateId,
            CustomerAggregateId = customerAggregateId,
            Comment = comment.Trim(),
            Rating = rating,
            IsPublished = false,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        // TODO: Register domain event: ReviewCreatedEvent

        return review;
    }

    public void Update(string comment, int rating)
    {
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment is required", nameof(comment));
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5", nameof(rating));
        if (IsPublished)
            throw new InvalidOperationException("Cannot update a published review");

        Comment = comment.Trim();
        Rating = rating;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: ReviewUpdatedEvent
    }

    public void Publish()
    {
        if (IsPublished)
            return;

        IsPublished = true;
        PublishedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: ReviewPublishedEvent
    }

    public void Unpublish()
    {
        if (!IsPublished)
            return;

        IsPublished = false;
        PublishedAt = null;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: ReviewUnpublishedEvent
    }

    public void Archive()
    {
        if (IsArchived)
            return;

        IsArchived = true;
        ArchivedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: ReviewArchivedEvent
    }
}
