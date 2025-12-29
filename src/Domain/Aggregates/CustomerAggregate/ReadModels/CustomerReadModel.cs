using Domain.Abstractions;
using Domain.Aggregates.BookingAggregate.ReadModels;

namespace Domain.Aggregates.CustomerAggregate.ReadModels;

public class CustomerReadModel : EntityReadModel
{
    public required Guid AggregateId { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Address { get; init; }
    public bool IsActive { get; init; }

    public List<BookingReadModel>? Bookings { get; init; }
}
