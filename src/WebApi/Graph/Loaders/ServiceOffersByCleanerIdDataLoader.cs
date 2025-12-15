using Application.Queries;
using Domain.Aggregates.BookingAggregate.ReadModels;
using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using MediatR;

namespace WebApi.Graph.Loaders;

public class ServiceOffersByCleanerIdDataLoader : GroupedDataLoader<Guid, ServiceOfferReadModel>
{
    private readonly IMediator _mediator;

    public ServiceOffersByCleanerIdDataLoader(
        IMediator mediator,
        IBatchScheduler scheduler,
        DataLoaderOptions options
        ) : base(scheduler, options)
        => _mediator = mediator;

    protected override Task<ILookup<Guid, ServiceOfferReadModel>> LoadGroupedBatchAsync(IReadOnlyList<Guid> cleanerAggregateIds, CancellationToken cancellationToken)
        => _mediator.Send(new GetOfferedServicesByCleanerAggregateIds { CleanerAggregateIds  = cleanerAggregateIds }, cancellationToken);
}
