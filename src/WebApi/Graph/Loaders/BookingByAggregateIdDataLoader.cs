using Application.Queries;
using Domain.Aggregates.BookingAggregate.ReadModels;
using MediatR;

namespace WebApi.Graph.Loaders;

public sealed class BookingByAggregateIdDataLoader : GroupedDataLoader<Guid, BookingReadModel>
{
    private readonly IMediator _mediator;

    public BookingByAggregateIdDataLoader(
        IMediator mediator,
        IBatchScheduler scheduler,
        DataLoaderOptions options
        ) : base(scheduler, options)
        => _mediator = mediator;

    protected override Task<ILookup<Guid, BookingReadModel>> LoadGroupedBatchAsync(IReadOnlyList<Guid> bookingAggregateIds, CancellationToken cancellationToken)
        => _mediator.Send(new GetBookingsByAggregateIds { BookingAggregateIds = bookingAggregateIds }, cancellationToken);
}

