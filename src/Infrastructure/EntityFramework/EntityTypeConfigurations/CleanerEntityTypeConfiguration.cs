using Domain.Aggregates.BookingAggregate;
using Domain.Aggregates.CleanerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.EntityTypeConfigurations;

internal class CleanerEntityTypeConfiguration : IEntityTypeConfiguration<Cleaner>
{
    public void Configure(EntityTypeBuilder<Cleaner> builder)
    {
        throw new NotImplementedException();
    }
}
