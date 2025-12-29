using Application.Queries;
using Domain.Aggregates.CleanerAggregate.ReadModels;
using MediatR;

namespace WebApi.Graph.Loaders;

public sealed class CleanerByAggregateIdDataLoader : GroupedDataLoader<Guid, CleanerReadModel>
{
    private readonly IMediator _mediator;

    public CleanerByAggregateIdDataLoader(
        IMediator mediator,
        IBatchScheduler scheduler,
        DataLoaderOptions options
        ) : base(scheduler, options)
        => _mediator = mediator;

    protected override Task<ILookup<Guid, CleanerReadModel>> LoadGroupedBatchAsync(IReadOnlyList<Guid> cleanerAggregateIds, CancellationToken cancellationToken)
        => _mediator.Send(new GetCleanersByAggregateIds { CleanerAggregateIds = cleanerAggregateIds }, cancellationToken);
}
