using Application.Queries;
using Domain.Aggregates.BookingAggregate.ReadModels;
using MediatR;

namespace WebApi.Graph.Loaders;

public sealed class BookingsByCustomerAggregateIdDataLoader : GroupedDataLoader<Guid, BookingReadModel>
{
    private readonly IMediator _mediator;

    public BookingsByCustomerAggregateIdDataLoader(
        IMediator mediator,
        IBatchScheduler scheduler,
        DataLoaderOptions options
        ) : base(scheduler, options)
        => _mediator = mediator;

    protected override Task<ILookup<Guid, BookingReadModel>> LoadGroupedBatchAsync(IReadOnlyList<Guid> customerAggregateIds, CancellationToken cancellationToken)
        => _mediator.Send(new GetBookingsByCustomerAggregateIds { CustomerAggregateIds = customerAggregateIds }, cancellationToken);
}

