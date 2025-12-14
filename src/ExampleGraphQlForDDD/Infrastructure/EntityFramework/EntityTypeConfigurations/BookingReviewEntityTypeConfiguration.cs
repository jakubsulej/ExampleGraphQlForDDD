using Domain.Aggregates.BookingAggregate;
using Domain.Aggregates.BookingReviewAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.EntityTypeConfigurations;

internal class BookingReviewEntityTypeConfiguration : IEntityTypeConfiguration<BookingReview>
{
    public void Configure(EntityTypeBuilder<BookingReview> builder)
    {
        throw new NotImplementedException();
    }
}
