using Domain.Abstractions;

namespace Domain.Aggregates.PaymentAggregate;

public class Payment : AggregateRoot
{
    public Guid BookingAggregateId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public PaymentMethod Method { get; private set; }
    public string? TransactionId { get; private set; }
    public DateTimeOffset? PaidAt { get; private set; }
    public string? FailureReason { get; private set; }

    // Private constructor for EF Core
    private Payment() { }

    // Factory method for creating new payments
    public static Payment Create(
        Guid aggregateId,
        Guid bookingAggregateId,
        decimal amount,
        PaymentMethod method)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentException("Aggregate ID cannot be empty", nameof(aggregateId));
        if (bookingAggregateId == Guid.Empty)
            throw new ArgumentException("Booking aggregate ID cannot be empty", nameof(bookingAggregateId));
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero", nameof(amount));

        var payment = new Payment
        {
            AggregateId = aggregateId,
            BookingAggregateId = bookingAggregateId,
            Amount = amount,
            Method = method,
            Status = PaymentStatus.Pending,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        // TODO: Register domain event: PaymentCreatedEvent

        return payment;
    }

    // Domain methods
    public void Process(string transactionId)
    {
        if (string.IsNullOrWhiteSpace(transactionId))
            throw new ArgumentException("Transaction ID is required", nameof(transactionId));
        if (Status != PaymentStatus.Pending)
            throw new InvalidOperationException($"Cannot process payment in {Status} status");

        Status = PaymentStatus.Processing;
        TransactionId = transactionId;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: PaymentProcessingEvent
    }

    public void Complete(string transactionId)
    {
        if (string.IsNullOrWhiteSpace(transactionId))
            throw new ArgumentException("Transaction ID is required", nameof(transactionId));
        if (Status != PaymentStatus.Processing && Status != PaymentStatus.Pending)
            throw new InvalidOperationException($"Cannot complete payment in {Status} status");

        Status = PaymentStatus.Completed;
        TransactionId = transactionId;
        PaidAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: PaymentCompletedEvent
    }

    public void Fail(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Failure reason is required", nameof(reason));
        if (Status == PaymentStatus.Completed)
            throw new InvalidOperationException("Cannot fail a completed payment");

        Status = PaymentStatus.Failed;
        FailureReason = reason;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: PaymentFailedEvent
    }

    public void Refund(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Refund reason is required", nameof(reason));
        if (Status != PaymentStatus.Completed)
            throw new InvalidOperationException("Can only refund completed payments");

        Status = PaymentStatus.Refunded;
        FailureReason = reason;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: PaymentRefundedEvent
    }
}

public enum PaymentStatus
{
    Pending = 0,
    Processing = 1,
    Completed = 2,
    Failed = 3,
    Refunded = 4
}

public enum PaymentMethod
{
    CreditCard = 0,
    DebitCard = 1,
    PayPal = 2,
    BankTransfer = 3,
    Cash = 4
}
