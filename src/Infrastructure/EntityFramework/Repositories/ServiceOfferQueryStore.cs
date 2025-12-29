using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using Domain.Aggregates.ServiceOfferAggregate.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Infrastructure.EntityFramework.Repositories;

internal class ServiceOfferQueryStore : IServiceOfferQueryStore
{
    private readonly IDbContextFactory<ServiceDbContext> _dbContextFactory;

    public ServiceOfferQueryStore(IDbContextFactory<ServiceDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    private static readonly Func<ServiceDbContext, int, int, IAsyncEnumerable<ServiceOfferReadModel>> GetServiceOffersQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, int page, int pageSize) =>
                dbContext.ServiceOffers
                    .AsNoTracking()
                    .OrderByDescending(o => o.Id)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .Select(b => new ServiceOfferReadModel
                    {
                        Id = b.Id,
                        AggregateId = b.AggregateId,
                        Title = b.Title,
                        Description = b.Description,
                        CleanerAggregateId = b.CleanerAggregateId,
                        ArchivedAt = b.ArchivedAt,
                        CreatedAt = b.CreatedAt,
                        IsArchived = b.IsArchived,
                        UpdatedAt = b.UpdatedAt,
                        ServicePricings = b.ServicePricings.Select(p => new ServicePricingReadModel
                        {
                            Price = p.Price,
                            PricingModel = p.PricingModel
                        }).ToList()
                    }));

    private static readonly Func<ServiceDbContext, IEnumerable<Guid>, IAsyncEnumerable<ServiceOfferReadModel>> GetServicesOfferByCleanerAggregateIdsQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, IEnumerable<Guid> cleanerAggregateIds) =>
                dbContext.ServiceOffers
                    .AsNoTracking()
                    .OrderByDescending(o => o.Id)
                    .Where(c => cleanerAggregateIds.Contains(c.CleanerAggregateId))
                    .Select(b => new ServiceOfferReadModel
                    {
                        Id = b.Id,
                        AggregateId = b.AggregateId,
                        Title = b.Title,
                        Description = b.Description,
                        CleanerAggregateId = b.CleanerAggregateId,
                        ArchivedAt = b.ArchivedAt,
                        CreatedAt = b.CreatedAt,
                        IsArchived = b.IsArchived,
                        UpdatedAt = b.UpdatedAt,
                        ServicePricings = b.ServicePricings.Select(p => new ServicePricingReadModel
                        {
                            Price = p.Price,
                            PricingModel = p.PricingModel
                        }).ToList()
                    }));

    public async Task<List<ServiceOfferReadModel>> GetServiceOffers(int page, int pageSize, CancellationToken cancellationToken)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var result = new List<ServiceOfferReadModel>();

        await foreach (var item in GetServiceOffersQuery(dbContext, page, pageSize).WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(item);
        }

        return result;
    }

    public async Task<int> GetServiceOffersCount(CancellationToken cancellationToken)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var count = await dbContext.ServiceOffers.CountAsync(cancellationToken);
        return count;
    }

    public async Task<List<ServiceOfferReadModel>> GetServiceOffersByCleanerAggregateIds(IEnumerable<Guid> cleanerAggregateIds, CancellationToken cancellationToken)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var result = new List<ServiceOfferReadModel>();

        await foreach (var item in GetServicesOfferByCleanerAggregateIdsQuery(dbContext, cleanerAggregateIds).WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(item);
        }

        return result;
    }
}
