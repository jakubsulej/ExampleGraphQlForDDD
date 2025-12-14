using Domain.Aggregates.BookingAggregate;
using Domain.Aggregates.BookingReviewAggregate;
using Domain.Aggregates.CleanerAggregate;
using Domain.Aggregates.CustomerAggregate;
using Domain.Aggregates.ServiceOfferAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework;

public class ServiceDbContext(DbContextOptions<ServiceDbContext> options) : DbContext(options)
{
    public DbSet<Booking> Bookings { get; private set; }
    public DbSet<Cleaner> Cleaners { get; private set; }
    public DbSet<Customer> Customers { get; private set; }
    public DbSet<BookingReview> BookingReviews { get; private set; }
    public DbSet<ServiceOffer> ServiceOffers { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceDbContext).Assembly);
    }
}
