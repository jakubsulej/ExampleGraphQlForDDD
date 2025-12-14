using Domain.Aggregates.BookingAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.EntityTypeConfigurations;

internal class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable(nameof(Booking), nameof(Booking));
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasColumnOrder(0);
        builder.Property(b => b.AggregateId)
            .HasColumnName($"{nameof(Booking)}{nameof(Booking.AggregateId)}")
            .HasColumnOrder(1);
        builder.HasIndex(b => b.AggregateId)
            .IsUnique();
        builder.Ignore(b => b.DomainEvents);

        builder.Property<byte[]>("RowVersion").IsRowVersion();
    }
}
