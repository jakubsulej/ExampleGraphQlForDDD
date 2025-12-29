using Domain.Aggregates.CleanerAggregate.ReadModels;
using Domain.Aggregates.CleanerAggregate.Repositories;
using MediatR;

namespace Application.Queries;

public class GetCleanersByAggregateIds : IRequest<ILookup<Guid, CleanerReadModel>>
{
    public required IReadOnlyList<Guid> CleanerAggregateIds { get; init; }
}

internal class GetCleanersByAggregateIdsRequestHandler : IRequestHandler<GetCleanersByAggregateIds, ILookup<Guid, CleanerReadModel>>
{
    private readonly ICleanerQueryStore _queryStore;

    public GetCleanersByAggregateIdsRequestHandler(ICleanerQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<ILookup<Guid, CleanerReadModel>> Handle(GetCleanersByAggregateIds request, CancellationToken cancellationToken)
    {
        var cleaners = await _queryStore.GetCleanersByAggregateIds(request.CleanerAggregateIds, cancellationToken);
        var lookup = cleaners.ToLookup(c => c.AggregateId);
        return lookup;
    }
}

