using Domain.Aggregates.BookingAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.EntityTypeConfigurations;

internal class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable(nameof(Booking));
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasColumnOrder(0);
        builder.Property(b => b.AggregateId)
            .HasColumnName($"{nameof(Booking)}{nameof(Booking.AggregateId)}")
            .HasColumnOrder(1);
        builder.HasIndex(b => b.AggregateId)
            .IsUnique();

        builder.Property(b => b.ServiceOfferAggregateId)
            .IsRequired();
        builder.Property(b => b.CustomerAggregateId)
            .IsRequired();
        builder.Property(b => b.Status)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(b => b.ScheduledDate);
        builder.Property(b => b.CompletedDate);

        // Configure owned value objects collection
        builder.OwnsMany(b => b.ServicePricingSnapshots, snapshot =>
        {
            snapshot.ToTable("ServicePricingSnapshots");
            snapshot.WithOwner().HasForeignKey("BookingId");
            snapshot.Property<int>("Id").ValueGeneratedOnAdd();
            snapshot.HasKey("Id");
            snapshot.Property(s => s.Price)
                .HasColumnName("Price")
                .IsRequired();
            snapshot.Property(s => s.PricingModel)
                .HasColumnName("PricingModel")
                .HasConversion<string>()
                .IsRequired();
            snapshot.Property(s => s.SnapshotDate)
                .HasColumnName("SnapshotDate")
                .IsRequired();
        });

        // Configure one-to-many relationship with BookingReview entity
        builder.HasMany(b => b.BookingReviews)
            .WithOne()
            .HasForeignKey("BookingId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(b => b.DomainEvents);
        builder.Property<byte[]>("RowVersion").IsRowVersion();
    }
}
