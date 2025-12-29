using Domain.Aggregates.BookingAggregate.ReadModels;
using Domain.Aggregates.BookingAggregate.Repositories;
using Domain.Aggregates.CustomerAggregate.ReadModels;
using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

internal class BookingQueryStore : IBookingQueryStore
{
    private readonly IDbContextFactory<ServiceDbContext> _dbContextFactory;

    public BookingQueryStore(IDbContextFactory<ServiceDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    private static readonly Func<ServiceDbContext, Guid, CancellationToken, Task<BookingReadModel?>> GetBookingByAggregateIdQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, Guid bookingAggregateId, CancellationToken cancellationToken) =>
                dbContext.Bookings
                    .AsNoTracking()
                    .Where(b => b.AggregateId == bookingAggregateId)
                    .Select(b => new BookingReadModel
                    {
                        Id = b.Id,
                        AggregateId = b.AggregateId,
                        ServiceOfferAggregateId = b.ServiceOfferAggregateId,
                        CustomerAggregateId = b.CustomerAggregateId,
                        CreatedAt = b.CreatedAt,
                        UpdatedAt = b.UpdatedAt,
                        ArchivedAt = b.ArchivedAt,
                        IsArchived = b.IsArchived,
                        ServicePricingSnapshots = b.ServicePricingSnapshots.Select(s => new ServicePricingSnapshotReadModel
                        {
                            Price = (int)s.Price,
                            PricingModel = s.PricingModel
                        }).ToList(),
                        BookingReviews = b.BookingReviews.Select(r => new BookingReviewReadModel
                        {
                            Id = r.Id,
                            BookingId = (int)b.Id,
                            Review = r.Comment,
                            Rating = r.Rating,
                            CreatedAt = r.CreatedAt,
                            UpdatedAt = r.UpdatedAt,
                            ArchivedAt = r.ArchivedAt,
                            IsArchived = r.IsArchived
                        }).ToList()
                    }).FirstOrDefault());

    private static readonly Func<ServiceDbContext, IEnumerable<Guid>, IAsyncEnumerable<BookingReadModel>> GetBookingsByAggregateIdsQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, IEnumerable<Guid> aggregateIds) =>
                dbContext.Bookings
                    .AsNoTracking()
                    .Where(b => aggregateIds.Contains(b.AggregateId))
                    .Select(b => new BookingReadModel
                    {
                        Id = b.Id,
                        AggregateId = b.AggregateId,
                        ServiceOfferAggregateId = b.ServiceOfferAggregateId,
                        CustomerAggregateId = b.CustomerAggregateId,
                        CreatedAt = b.CreatedAt,
                        UpdatedAt = b.UpdatedAt,
                        ArchivedAt = b.ArchivedAt,
                        IsArchived = b.IsArchived,
                        ServicePricingSnapshots = b.ServicePricingSnapshots.Select(s => new ServicePricingSnapshotReadModel
                        {
                            Price = (int)s.Price,
                            PricingModel = s.PricingModel
                        }).ToList(),
                        BookingReviews = b.BookingReviews.Select(r => new BookingReviewReadModel
                        {
                            Id = r.Id,
                            BookingId = (int)b.Id,
                            Review = r.Comment,
                            Rating = r.Rating,
                            CreatedAt = r.CreatedAt,
                            UpdatedAt = r.UpdatedAt,
                            ArchivedAt = r.ArchivedAt,
                            IsArchived = r.IsArchived
                        }).ToList()
                    }));

    private static readonly Func<ServiceDbContext, Guid, IAsyncEnumerable<BookingReadModel>> GetBookingsByCustomerAggregateIdQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, Guid customerAggregateId) =>
                dbContext.Bookings
                    .AsNoTracking()
                    .Where(b => b.CustomerAggregateId == customerAggregateId)
                    .OrderByDescending(b => b.CreatedAt)
                    .Select(b => new BookingReadModel
                    {
                        Id = b.Id,
                        AggregateId = b.AggregateId,
                        ServiceOfferAggregateId = b.ServiceOfferAggregateId,
                        CustomerAggregateId = b.CustomerAggregateId,
                        CreatedAt = b.CreatedAt,
                        UpdatedAt = b.UpdatedAt,
                        ArchivedAt = b.ArchivedAt,
                        IsArchived = b.IsArchived,
                        ServicePricingSnapshots = b.ServicePricingSnapshots.Select(s => new ServicePricingSnapshotReadModel
                        {
                            Price = (int)s.Price,
                            PricingModel = s.PricingModel
                        }).ToList(),
                        BookingReviews = b.BookingReviews.Select(r => new BookingReviewReadModel
                        {
                            Id = r.Id,
                            BookingId = (int)b.Id,
                            Review = r.Comment,
                            Rating = r.Rating,
                            CreatedAt = r.CreatedAt,
                            UpdatedAt = r.UpdatedAt,
                            ArchivedAt = r.ArchivedAt,
                            IsArchived = r.IsArchived
                        }).ToList()
                    }));

    private static readonly Func<ServiceDbContext, int, int, IAsyncEnumerable<BookingReadModel>> GetBookingsQuery =
        EF.CompileAsyncQuery(
            (ServiceDbContext dbContext, int page, int pageSize) =>
                dbContext.Bookings
                    .AsNoTracking()
                    .OrderByDescending(b => b.CreatedAt)
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .Select(b => new BookingReadModel
                    {
                        Id = b.Id,
                        AggregateId = b.AggregateId,
                        ServiceOfferAggregateId = b.ServiceOfferAggregateId,
                        CustomerAggregateId = b.CustomerAggregateId,
                        CreatedAt = b.CreatedAt,
                        UpdatedAt = b.UpdatedAt,
                        ArchivedAt = b.ArchivedAt,
                        IsArchived = b.IsArchived,
                        ServicePricingSnapshots = b.ServicePricingSnapshots.Select(s => new ServicePricingSnapshotReadModel
                        {
                            Price = (int)s.Price,
                            PricingModel = s.PricingModel
                        }).ToList(),
                        BookingReviews = b.BookingReviews.Select(r => new BookingReviewReadModel
                        {
                            Id = r.Id,
                            BookingId = (int)b.Id,
                            Review = r.Comment,
                            Rating = r.Rating,
                            CreatedAt = r.CreatedAt,
                            UpdatedAt = r.UpdatedAt,
                            ArchivedAt = r.ArchivedAt,
                            IsArchived = r.IsArchived
                        }).ToList()
                    }));

    public async Task<BookingReadModel?> GetBookingByAggregateId(Guid bookingAggregateId, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await GetBookingByAggregateIdQuery(dbContext, bookingAggregateId, cancellationToken);
    }

    public async Task<List<BookingReadModel>> GetBookingsByAggregateIds(IEnumerable<Guid> bookingAggregateIds, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var result = new List<BookingReadModel>();

        await foreach (var item in GetBookingsByAggregateIdsQuery(dbContext, bookingAggregateIds).WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(item);
        }

        return result;
    }

    public async Task<List<BookingReadModel>> GetBookingsByCustomerAggregateId(Guid customerAggregateId, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var result = new List<BookingReadModel>();

        await foreach (var item in GetBookingsByCustomerAggregateIdQuery(dbContext, customerAggregateId).WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(item);
        }

        return result;
    }

    public async Task<List<BookingReadModel>> GetBookings(int page, int pageSize, CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var result = new List<BookingReadModel>();

        await foreach (var item in GetBookingsQuery(dbContext, page, pageSize).WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            result.Add(item);
        }

        return result;
    }

    public async Task<int> GetBookingsCount(CancellationToken cancellationToken)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Bookings.CountAsync(cancellationToken);
    }
}
