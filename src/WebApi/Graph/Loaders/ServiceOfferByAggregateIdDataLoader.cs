using Application.Queries;
using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using MediatR;

namespace WebApi.Graph.Loaders;

public sealed class ServiceOfferByAggregateIdDataLoader : GroupedDataLoader<Guid, ServiceOfferReadModel>
{
    private readonly IMediator _mediator;

    public ServiceOfferByAggregateIdDataLoader(
        IMediator mediator,
        IBatchScheduler scheduler,
        DataLoaderOptions options
        ) : base(scheduler, options)
        => _mediator = mediator;

    protected override Task<ILookup<Guid, ServiceOfferReadModel>> LoadGroupedBatchAsync(IReadOnlyList<Guid> serviceOfferAggregateIds, CancellationToken cancellationToken)
        => _mediator.Send(new GetServiceOffersByAggregateIds { ServiceOfferAggregateIds = serviceOfferAggregateIds }, cancellationToken);
}

