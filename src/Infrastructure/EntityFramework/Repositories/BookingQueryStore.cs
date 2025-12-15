using Domain.Aggregates.BookingAggregate.ReadModels;
using Domain.Aggregates.BookingAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

internal class BookingQueryStore : IBookingQueryStore
{
    private readonly IDbContextFactory<ServiceDbContext> _dbContextFactory;

    public BookingQueryStore(IDbContextFactory<ServiceDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    //private static readonly Func<ServiceDbContext, IEnumerable<Guid>, IAsyncEnumerable<BookingReadModel>>
    //    GetBookingsBy =
    //        EF.CompileAsyncQuery (
    //            ServiceDbContext dbContext, IEnumerable<Guid> aggregateIds) =>
    //        {

    //        };

    //public async Task<List<BookingReadModel>> GetBookingsBy(IEnumerable<Guid> aggregateIds, CancellationToken cancellationToken)
    //{

    //}
}
