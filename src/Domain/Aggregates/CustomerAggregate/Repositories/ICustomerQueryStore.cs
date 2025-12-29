using Domain.Aggregates.CustomerAggregate.ReadModels;

namespace Domain.Aggregates.CustomerAggregate.Repositories;

public interface ICustomerQueryStore
{
    Task<CustomerReadModel?> GetCustomerByAggregateId(Guid customerAggregateId, CancellationToken cancellationToken);
    Task<List<CustomerReadModel>> GetCustomersByAggregateIds(IEnumerable<Guid> customerAggregateIds, CancellationToken cancellationToken);
    Task<List<CustomerReadModel>> GetCustomers(int page, int pageSize, CancellationToken cancellationToken);
    Task<int> GetCustomersCount(CancellationToken cancellationToken);
}
