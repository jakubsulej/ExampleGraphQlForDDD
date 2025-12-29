using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using Domain.Aggregates.ServiceOfferAggregate.Repositories;
using MediatR;

namespace Application.Queries;

public class GetServiceOffersByAggregateIds : IRequest<ILookup<Guid, ServiceOfferReadModel>>
{
    public required IReadOnlyList<Guid> ServiceOfferAggregateIds { get; init; }
}

internal class GetServiceOffersByAggregateIdsRequestHandler : IRequestHandler<GetServiceOffersByAggregateIds, ILookup<Guid, ServiceOfferReadModel>>
{
    private readonly IServiceOfferQueryStore _queryStore;

    public GetServiceOffersByAggregateIdsRequestHandler(IServiceOfferQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<ILookup<Guid, ServiceOfferReadModel>> Handle(GetServiceOffersByAggregateIds request, CancellationToken cancellationToken)
    {
        var serviceOffers = await _queryStore.GetServiceOffersByAggregateIds(request.ServiceOfferAggregateIds, cancellationToken);
        var lookup = serviceOffers.ToLookup(so => so.AggregateId);
        return lookup;
    }
}

