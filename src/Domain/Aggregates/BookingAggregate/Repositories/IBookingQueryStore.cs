using Domain.Aggregates.BookingAggregate.ReadModels;

namespace Domain.Aggregates.BookingAggregate.Repositories;

public interface IBookingQueryStore
{
    Task<BookingReadModel?> GetBookingByAggregateId(Guid bookingAggregateId, CancellationToken cancellationToken);
    Task<List<BookingReadModel>> GetBookingsByAggregateIds(IEnumerable<Guid> bookingAggregateIds, CancellationToken cancellationToken);
    Task<List<BookingReadModel>> GetBookingsByCustomerAggregateId(Guid customerAggregateId, CancellationToken cancellationToken);
    Task<List<BookingReadModel>> GetBookings(int page, int pageSize, CancellationToken cancellationToken);
    Task<int> GetBookingsCount(CancellationToken cancellationToken);
}
