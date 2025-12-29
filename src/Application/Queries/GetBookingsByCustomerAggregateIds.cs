using Domain.Aggregates.BookingAggregate.ReadModels;
using Domain.Aggregates.BookingAggregate.Repositories;
using MediatR;

namespace Application.Queries;

public class GetBookingsByCustomerAggregateIds : IRequest<ILookup<Guid, BookingReadModel>>
{
    public required IReadOnlyList<Guid> CustomerAggregateIds { get; init; }
}

internal class GetBookingsByCustomerAggregateIdsRequestHandler : IRequestHandler<GetBookingsByCustomerAggregateIds, ILookup<Guid, BookingReadModel>>
{
    private readonly IBookingQueryStore _queryStore;

    public GetBookingsByCustomerAggregateIdsRequestHandler(IBookingQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<ILookup<Guid, BookingReadModel>> Handle(GetBookingsByCustomerAggregateIds request, CancellationToken cancellationToken)
    {
        var allBookings = new List<BookingReadModel>();
        
        foreach (var customerAggregateId in request.CustomerAggregateIds)
        {
            var bookings = await _queryStore.GetBookingsByCustomerAggregateId(customerAggregateId, cancellationToken);
            allBookings.AddRange(bookings);
        }

        var lookup = allBookings.ToLookup(b => b.CustomerAggregateId);
        return lookup;
    }
}

