using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using Domain.Aggregates.ServiceOfferAggregate.Repositories;
using MediatR;

namespace Application.Queries;

public record GetServiceOffersPage : IRequest<GetServiceOffersPageResponse>
{
    public required int Page { get; init; }
    public required int PageSize { get; init; }
}

public record GetServiceOffersPageResponse
{
    public required IEnumerable<ServiceOfferReadModel> ServiceOffers { get; init; }
    public required int TotalCount { get; init; }
}

internal class GetServiceOffersRequestHandler : IRequestHandler<GetServiceOffersPage, GetServiceOffersPageResponse>
{
    private readonly IServiceOfferQueryStore _queryStore;

    public GetServiceOffersRequestHandler(IServiceOfferQueryStore queryStore)
    {
        _queryStore = queryStore;
    }

    public async Task<GetServiceOffersPageResponse> Handle(GetServiceOffersPage request, CancellationToken cancellationToken)
    {
        var serviceOffers = await _queryStore.GetServiceOffers(request.Page, request.PageSize, cancellationToken);
        var serviceOffersCount = await _queryStore.GetServiceOffersCount(cancellationToken);

        return new GetServiceOffersPageResponse
        {
            ServiceOffers = serviceOffers,
            TotalCount = serviceOffersCount,
        };
    }
}
