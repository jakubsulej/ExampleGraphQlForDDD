using Domain.Aggregates.BookingAggregate.ReadModels;
using Domain.Aggregates.BookingAggregate.Repositories;
using Domain.Shared.Exceptions;
using MediatR;

namespace Application.Queries;

public class GetBookingByAggregateId : IRequest<BookingReadModel>
{
    public required Guid BookingAggregateId { get; init; }
}

internal class GetBookingByAggregateIdRequestHandler : IRequestHandler<GetBookingByAggregateId, BookingReadModel>
{
    private readonly IBookingQueryStore _queryStore;

    public GetBookingByAggregateIdRequestHandler(IBookingQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<BookingReadModel> Handle(GetBookingByAggregateId request, CancellationToken cancellationToken)
    {
        var bookingReadModel = await _queryStore.GetBookingByAggregateId(request.BookingAggregateId, cancellationToken);
        if (bookingReadModel == null) 
            throw new EntityNotFoundException($"Booking with aggregate ID {request.BookingAggregateId} was not found");

        return bookingReadModel;
    }
}

