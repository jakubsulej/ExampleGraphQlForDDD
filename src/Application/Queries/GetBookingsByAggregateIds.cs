using Domain.Aggregates.BookingAggregate.ReadModels;
using Domain.Aggregates.BookingAggregate.Repositories;
using MediatR;

namespace Application.Queries;

public class GetBookingsByAggregateIds : IRequest<ILookup<Guid, BookingReadModel>>
{
    public required IReadOnlyList<Guid> BookingAggregateIds { get; init; }
}

internal class GetBookingsByAggregateIdsRequestHandler : IRequestHandler<GetBookingsByAggregateIds, ILookup<Guid, BookingReadModel>>
{
    private readonly IBookingQueryStore _queryStore;

    public GetBookingsByAggregateIdsRequestHandler(IBookingQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<ILookup<Guid, BookingReadModel>> Handle(GetBookingsByAggregateIds request, CancellationToken cancellationToken)
    {
        var bookings = await _queryStore.GetBookingsByAggregateIds(request.BookingAggregateIds, cancellationToken);
        var lookup = bookings.ToLookup(b => b.AggregateId);
        return lookup;
    }
}

