using Domain.Aggregates.ServiceOfferAggregate.ReadModels;

namespace Domain.Aggregates.ServiceOfferAggregate.Repositories;

public interface IServiceOfferQueryStore
{
    Task<List<ServiceOfferReadModel>> GetServiceOffers(int page, int pageSize, CancellationToken cancellationToken);
    Task<List<ServiceOfferReadModel>> GetServiceOffersByCleanerAggregateIds(IEnumerable<Guid> cleanerAggregateIds, CancellationToken cancellationToken);
    Task<int> GetServiceOffersCount(CancellationToken cancellationToken);
}
