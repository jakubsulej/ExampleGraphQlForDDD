using Domain.Aggregates.CleanerAggregate.ReadModels;
using Domain.Aggregates.CleanerAggregate.Repositories;
using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

internal class CleanerQueryStore : ICleanerQueryStore
{
    private readonly IDbContextFactory<ServiceDbContext> _dbContextFactory;

    public CleanerQueryStore(IDbContextFactory<ServiceDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    private static readonly Func<ServiceDbContext, Guid, CancellationToken, Task<CleanerReadModel?>> GetCleanerByAggregateIdQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, Guid cleanerAggregateId, CancellationToken cancellationToken) =>
                dbContext.Cleaners
                    .AsNoTracking()
                    .Where(p => p.AggregateId == cleanerAggregateId)    
                    .Select(b => new CleanerReadModel
                    {
                        Id = b.Id,
                        AggregateId = b.AggregateId,
                        Name = b.Name,
                        Description = b.Description,
                        ArchivedAt = b.ArchivedAt,
                        CreatedAt = b.CreatedAt,
                        IsArchived = b.IsArchived,
                        UpdatedAt = b.UpdatedAt
                    }).FirstOrDefault());

    private static readonly Func<ServiceDbContext, IEnumerable<Guid>, IAsyncEnumerable<CleanerReadModel>> GetCleanersByAggregateIdsQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, IEnumerable<Guid> aggregateIds) =>
                dbContext.Cleaners
                    .AsNoTracking()
                    .Where(c => aggregateIds.Contains(c.AggregateId))
                    .Select(b => new CleanerReadModel
                    {
                        Id = b.Id,
                        AggregateId = b.AggregateId,
                        Name = b.Name,
                        Description = b.Description,
                        ArchivedAt = b.ArchivedAt,
                        CreatedAt = b.CreatedAt,
                        IsArchived = b.IsArchived,
                        UpdatedAt = b.UpdatedAt
                    }));

    private static readonly Func<ServiceDbContext, int, int, IAsyncEnumerable<CleanerReadModel>> GetCleanersQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, int page, int pageSize) =>
                dbContext.Cleaners
                    .AsNoTracking()
                    .OrderByDescending(o => o.Id)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .Select(b => new CleanerReadModel
                    {
                        Id = b.Id,
                        AggregateId = b.AggregateId,
                        Name = b.Name,
                        Description = b.Description,
                        ArchivedAt = b.ArchivedAt,
                        CreatedAt = b.CreatedAt,
                        IsArchived = b.IsArchived,
                        UpdatedAt = b.UpdatedAt
                    }));

    public async Task<CleanerReadModel?> GetCleanerByAggregateId(Guid cleanerAggregateId, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await GetCleanerByAggregateIdQuery(dbContext, cleanerAggregateId, cancellationToken);
    }

    public async Task<List<CleanerReadModel>> GetCleanersByAggregateIds(IEnumerable<Guid> cleanerAggregateIds, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var result = new List<CleanerReadModel>();

        await foreach (var item in GetCleanersByAggregateIdsQuery(dbContext, cleanerAggregateIds).WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(item);
        }

        return result;
    }

    public async Task<List<CleanerReadModel>> GetCleaners(int page, int pageSize, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var result = new List<CleanerReadModel>();

        await foreach (var item in GetCleanersQuery(dbContext, page, pageSize).WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(item);
        }

        return result;
    }

    public async Task<int> GetCleanersCount(CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var count = await dbContext.Cleaners.CountAsync(cancellationToken);
        return count;
    }
}
