using Domain.Abstractions;
using Domain.Aggregates.BookingAggregate.Entities;
using Domain.Aggregates.BookingAggregate.Enums;
using Domain.Aggregates.BookingAggregate.ValueObjects;
using System.Collections.ObjectModel;

namespace Domain.Aggregates.BookingAggregate;

public class Booking : AggregateRoot
{
    private readonly List<ServicePricingSnapshot> _servicePricingSnapshots = [];
    private readonly List<BookingReview> _bookingReviews = [];

    public Guid ServiceOfferAggregateId { get; private set; }
    public Guid CustomerAggregateId { get; private set; }
    public BookingStatus Status { get; private set; }
    public DateTimeOffset? ScheduledDate { get; private set; }
    public DateTimeOffset? CompletedDate { get; private set; }

    public ReadOnlyCollection<ServicePricingSnapshot> ServicePricingSnapshots => _servicePricingSnapshots.AsReadOnly();
    public ReadOnlyCollection<BookingReview> BookingReviews => _bookingReviews.AsReadOnly();

    private Booking() { }

    public static Booking Create(
        Guid aggregateId,
        Guid serviceOfferAggregateId,
        Guid customerAggregateId,
        DateTimeOffset scheduledDate,
        List<ServicePricingSnapshot> pricingSnapshots)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentException("Aggregate ID cannot be empty", nameof(aggregateId));
        if (serviceOfferAggregateId == Guid.Empty)
            throw new ArgumentException("Service offer aggregate ID cannot be empty", nameof(serviceOfferAggregateId));
        if (customerAggregateId == Guid.Empty)
            throw new ArgumentException("Customer aggregate ID cannot be empty", nameof(customerAggregateId));
        if (pricingSnapshots == null || pricingSnapshots.Count == 0)
            throw new ArgumentException("At least one pricing snapshot is required", nameof(pricingSnapshots));
        if (scheduledDate <= DateTimeOffset.UtcNow)
            throw new ArgumentException("Scheduled date must be in the future", nameof(scheduledDate));

        var booking = new Booking
        {
            AggregateId = aggregateId,
            ServiceOfferAggregateId = serviceOfferAggregateId,
            CustomerAggregateId = customerAggregateId,
            Status = BookingStatus.Pending,
            ScheduledDate = scheduledDate,
            CreatedAt = DateTimeOffset.UtcNow,
            ArchivedAt = default(DateTimeOffset),
            UpdatedAt = DateTimeOffset.UtcNow
        };

        booking._servicePricingSnapshots.AddRange(pricingSnapshots);

        // TODO: Register domain event: BookingCreatedEvent

        return booking;
    }

    public void Confirm()
    {
        if (Status != BookingStatus.Pending)
            throw new InvalidOperationException($"Cannot confirm booking in {Status} status");

        Status = BookingStatus.Confirmed;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: BookingConfirmedEvent
    }

    public void Complete()
    {
        if (Status != BookingStatus.Confirmed && Status != BookingStatus.InProgress)
            throw new InvalidOperationException($"Cannot complete booking in {Status} status");

        Status = BookingStatus.Completed;
        CompletedDate = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: BookingCompletedEvent
    }

    public void Cancel(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Cancellation reason is required", nameof(reason));

        if (Status == BookingStatus.Completed || Status == BookingStatus.Cancelled)
            throw new InvalidOperationException($"Cannot cancel booking in {Status} status");

        Status = BookingStatus.Cancelled;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: BookingCancelledEvent
    }

    public void AddReview(Guid reviewAggregateId, string comment, int rating)
    {
        if (Status != BookingStatus.Completed)
            throw new InvalidOperationException("Can only add review to completed bookings");

        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5", nameof(rating));

        // Note: This creates a BookingReview entity within the Booking aggregate
        // The Review aggregate should be created separately and linked via ReviewAggregateId
        var review = BookingReview.Create(reviewAggregateId, AggregateId, comment, rating);
        _bookingReviews.Add(review);
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: ReviewAddedToBookingEvent
    }

    public void Reschedule(DateTimeOffset newScheduledDate)
    {
        if (Status == BookingStatus.Completed || Status == BookingStatus.Cancelled)
            throw new InvalidOperationException($"Cannot reschedule booking in {Status} status");

        ScheduledDate = newScheduledDate;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: BookingRescheduledEvent
    }
}
