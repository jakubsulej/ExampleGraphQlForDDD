using Domain.Aggregates.BookingAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.EntityTypeConfigurations;

internal class BookingReviewEntityTypeConfiguration : IEntityTypeConfiguration<BookingReview>
{
    public void Configure(EntityTypeBuilder<BookingReview> builder)
    {
        builder.ToTable(nameof(BookingReview), nameof(BookingReview));
        builder.HasKey(br => br.Id);
        builder.Property(br => br.Id).HasColumnOrder(0);

        builder.Property(br => br.BookingAggregateId)
            .IsRequired();
        builder.Property(br => br.ReviewAggregateId)
            .IsRequired();
        builder.Property(br => br.Comment)
            .IsRequired()
            .HasMaxLength(2000);
        builder.Property(br => br.Rating)
            .IsRequired();

        builder.HasIndex(br => br.BookingAggregateId);
        builder.HasIndex(br => br.ReviewAggregateId);
    }
}
