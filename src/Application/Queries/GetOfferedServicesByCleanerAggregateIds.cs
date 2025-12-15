using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using Domain.Aggregates.ServiceOfferAggregate.Repositories;
using MediatR;

namespace Application.Queries;

public class GetOfferedServicesByCleanerAggregateIds : IRequest<ILookup<Guid, ServiceOfferReadModel>>
{
    public required IReadOnlyList<Guid> CleanerAggregateIds { get; init; }
}

internal class GetOfferedServicesByCleanerAggregateIdsRequestHandler : IRequestHandler<GetOfferedServicesByCleanerAggregateIds, ILookup<Guid, ServiceOfferReadModel>>
{
    private readonly IServiceOfferQueryStore _queryStore;

    public GetOfferedServicesByCleanerAggregateIdsRequestHandler(IServiceOfferQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<ILookup<Guid, ServiceOfferReadModel>> Handle(GetOfferedServicesByCleanerAggregateIds request, CancellationToken cancellationToken)
    {
        var cleanerReadModels = await _queryStore.GetServiceOffersByCleanerAggregateIds(request.CleanerAggregateIds, cancellationToken);
        var lookup = cleanerReadModels.ToLookup(e => e.CleanerAggregateId);
        return lookup;
    }
}
