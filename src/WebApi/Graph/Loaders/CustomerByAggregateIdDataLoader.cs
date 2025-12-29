using Application.Queries;
using Domain.Aggregates.CustomerAggregate.ReadModels;
using MediatR;

namespace WebApi.Graph.Loaders;

public sealed class CustomerByAggregateIdDataLoader : GroupedDataLoader<Guid, CustomerReadModel>
{
    private readonly IMediator _mediator;

    public CustomerByAggregateIdDataLoader(
        IMediator mediator,
        IBatchScheduler scheduler,
        DataLoaderOptions options
        ) : base(scheduler, options)
        => _mediator = mediator;

    protected override Task<ILookup<Guid, CustomerReadModel>> LoadGroupedBatchAsync(IReadOnlyList<Guid> customerAggregateIds, CancellationToken cancellationToken)
        => _mediator.Send(new GetCustomersByAggregateIds { CustomerAggregateIds = customerAggregateIds }, cancellationToken);
}

