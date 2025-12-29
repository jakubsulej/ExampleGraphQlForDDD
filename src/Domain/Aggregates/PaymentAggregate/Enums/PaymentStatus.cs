namespace Domain.Aggregates.PaymentAggregate.Enums;

public enum PaymentStatus
{
    Undefined = 0,
    Pending = 1,
    Processing = 2,
    Completed = 3,
    Failed = 4,
    Refunded = 5
}
