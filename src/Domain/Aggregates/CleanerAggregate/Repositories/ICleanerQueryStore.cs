using Domain.Aggregates.CleanerAggregate.ReadModels;

namespace Domain.Aggregates.CleanerAggregate.Repositories;

public interface ICleanerQueryStore
{
    Task<CleanerReadModel?> GetCleanerByAggregateId(Guid cleanerAggregateId, CancellationToken cancellationToken);
    Task<List<CleanerReadModel>> GetCleaners(int page, int pageSize, CancellationToken cancellationToken);
    Task<int> GetCleanersCount(CancellationToken cancellationToken);
}
