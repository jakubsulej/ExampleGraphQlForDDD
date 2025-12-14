using Domain.Aggregates.BookingAggregate.ReadModels;

namespace Domain.Aggregates.CustomerAggregate.ReadModels;

public class CustomerReadModel
{
    public required string Name { get; set; }
    public required string Email { get; set; }

    public List<BookingReadModel>? Bookings { get; set; }
}
