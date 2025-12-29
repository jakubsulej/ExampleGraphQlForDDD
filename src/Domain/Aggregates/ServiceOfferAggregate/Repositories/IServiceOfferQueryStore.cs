using Domain.Aggregates.ServiceOfferAggregate.ReadModels;

namespace Domain.Aggregates.ServiceOfferAggregate.Repositories;

public interface IServiceOfferQueryStore
{
    Task<ServiceOfferReadModel?> GetServiceOfferByAggregateId(Guid serviceOfferAggregateId, CancellationToken cancellationToken);
    Task<List<ServiceOfferReadModel>> GetServiceOffersByAggregateIds(IEnumerable<Guid> serviceOfferAggregateIds, CancellationToken cancellationToken);
    Task<List<ServiceOfferReadModel>> GetServiceOffers(int page, int pageSize, CancellationToken cancellationToken);
    Task<List<ServiceOfferReadModel>> GetServiceOffersByCleanerAggregateIds(IEnumerable<Guid> cleanerAggregateIds, CancellationToken cancellationToken);
    Task<int> GetServiceOffersCount(CancellationToken cancellationToken);
}
