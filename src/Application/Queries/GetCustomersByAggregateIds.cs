using Domain.Aggregates.CustomerAggregate.ReadModels;
using Domain.Aggregates.CustomerAggregate.Repositories;
using MediatR;

namespace Application.Queries;

public class GetCustomersByAggregateIds : IRequest<ILookup<Guid, CustomerReadModel>>
{
    public required IReadOnlyList<Guid> CustomerAggregateIds { get; init; }
}

internal class GetCustomersByAggregateIdsRequestHandler : IRequestHandler<GetCustomersByAggregateIds, ILookup<Guid, CustomerReadModel>>
{
    private readonly ICustomerQueryStore _queryStore;

    public GetCustomersByAggregateIdsRequestHandler(ICustomerQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<ILookup<Guid, CustomerReadModel>> Handle(GetCustomersByAggregateIds request, CancellationToken cancellationToken)
    {
        var customers = await _queryStore.GetCustomersByAggregateIds(request.CustomerAggregateIds, cancellationToken);
        var lookup = customers.ToLookup(c => c.AggregateId);
        return lookup;
    }
}

