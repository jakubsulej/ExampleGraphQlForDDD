namespace Domain.Aggregates.BookingAggregate.Enums;

public enum BookingStatus
{
    Undefined = 0,
    Pending = 1,
    Confirmed = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5
}
