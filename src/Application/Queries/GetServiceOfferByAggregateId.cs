using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using Domain.Aggregates.ServiceOfferAggregate.Repositories;
using Domain.Shared.Exceptions;
using MediatR;

namespace Application.Queries;

public class GetServiceOfferByAggregateId : IRequest<ServiceOfferReadModel>
{
    public required Guid ServiceOfferAggregateId { get; init; }
}

internal class GetServiceOfferByAggregateIdRequestHandler : IRequestHandler<GetServiceOfferByAggregateId, ServiceOfferReadModel>
{
    private readonly IServiceOfferQueryStore _queryStore;

    public GetServiceOfferByAggregateIdRequestHandler(IServiceOfferQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<ServiceOfferReadModel> Handle(GetServiceOfferByAggregateId request, CancellationToken cancellationToken)
    {
        var serviceOfferReadModel = await _queryStore.GetServiceOfferByAggregateId(request.ServiceOfferAggregateId, cancellationToken);
        if (serviceOfferReadModel == null) 
            throw new EntityNotFoundException($"Service offer with aggregate ID {request.ServiceOfferAggregateId} was not found");

        return serviceOfferReadModel;
    }
}

