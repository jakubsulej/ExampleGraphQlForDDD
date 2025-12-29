using Domain.Aggregates.BookingAggregate.ReadModels;
using Domain.Aggregates.BookingAggregate.Repositories;
using MediatR;

namespace Application.Queries;

public record GetBookingsPage : IRequest<GetBookingsPageResponse>
{
    public required int Page { get; init; }
    public required int PageSize { get; init; }
}

public record GetBookingsPageResponse
{
    public required IEnumerable<BookingReadModel> Bookings { get; init; }
    public required int TotalCount { get; init; }
}

internal class GetBookingsPageRequestHandler : IRequestHandler<GetBookingsPage, GetBookingsPageResponse>
{
    private readonly IBookingQueryStore _queryStore;

    public GetBookingsPageRequestHandler(IBookingQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<GetBookingsPageResponse> Handle(GetBookingsPage request, CancellationToken cancellationToken)
    {
        var bookings = await _queryStore.GetBookings(request.Page, request.PageSize, cancellationToken);
        var bookingsCount = await _queryStore.GetBookingsCount(cancellationToken);

        return new GetBookingsPageResponse
        {
            Bookings = bookings,
            TotalCount = bookingsCount,
        };
    }
}

