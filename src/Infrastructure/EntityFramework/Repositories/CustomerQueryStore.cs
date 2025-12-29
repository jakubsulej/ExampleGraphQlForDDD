using Domain.Aggregates.CustomerAggregate.ReadModels;
using Domain.Aggregates.CustomerAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

internal class CustomerQueryStore : ICustomerQueryStore
{
    private readonly IDbContextFactory<ServiceDbContext> _dbContextFactory;

    public CustomerQueryStore(IDbContextFactory<ServiceDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    private static readonly Func<ServiceDbContext, Guid, CancellationToken, Task<CustomerReadModel?>> GetCustomerByAggregateIdQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, Guid customerAggregateId, CancellationToken cancellationToken) =>
                dbContext.Customers
                    .AsNoTracking()
                    .Where(c => c.AggregateId == customerAggregateId)
                    .Select(c => new CustomerReadModel
                    {
                        Id = c.Id,
                        AggregateId = c.AggregateId,
                        Name = c.Name,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                        Address = c.Address,
                        IsActive = c.IsActive,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt,
                        ArchivedAt = c.ArchivedAt,
                        IsArchived = c.IsArchived
                    }).FirstOrDefault());

    private static readonly Func<ServiceDbContext, IEnumerable<Guid>, IAsyncEnumerable<CustomerReadModel>> GetCustomersByAggregateIdsQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, IEnumerable<Guid> aggregateIds) =>
                dbContext.Customers
                    .AsNoTracking()
                    .Where(c => aggregateIds.Contains(c.AggregateId))
                    .Select(c => new CustomerReadModel
                    {
                        Id = c.Id,
                        AggregateId = c.AggregateId,
                        Name = c.Name,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                        Address = c.Address,
                        IsActive = c.IsActive,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt,
                        ArchivedAt = c.ArchivedAt,
                        IsArchived = c.IsArchived
                    }));

    private static readonly Func<ServiceDbContext, int, int, IAsyncEnumerable<CustomerReadModel>> GetCustomersQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, int page, int pageSize) =>
                dbContext.Customers
                    .AsNoTracking()
                    .OrderByDescending(c => c.CreatedAt)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .Select(c => new CustomerReadModel
                    {
                        Id = c.Id,
                        AggregateId = c.AggregateId,
                        Name = c.Name,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                        Address = c.Address,
                        IsActive = c.IsActive,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt,
                        ArchivedAt = c.ArchivedAt,
                        IsArchived = c.IsArchived
                    }));

    public async Task<CustomerReadModel?> GetCustomerByAggregateId(Guid customerAggregateId, CancellationToken cancellationToken)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await GetCustomerByAggregateIdQuery(dbContext, customerAggregateId, cancellationToken);
    }

    public async Task<List<CustomerReadModel>> GetCustomersByAggregateIds(IEnumerable<Guid> customerAggregateIds, CancellationToken cancellationToken)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var result = new List<CustomerReadModel>();

        await foreach (var item in GetCustomersByAggregateIdsQuery(dbContext, customerAggregateIds).WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(item);
        }

        return result;
    }

    public async Task<List<CustomerReadModel>> GetCustomers(int page, int pageSize, CancellationToken cancellationToken)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var result = new List<CustomerReadModel>();

        await foreach (var item in GetCustomersQuery(dbContext, page, pageSize).WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(item);
        }

        return result;
    }

    public async Task<int> GetCustomersCount(CancellationToken cancellationToken)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Customers.CountAsync(cancellationToken);
    }
}
