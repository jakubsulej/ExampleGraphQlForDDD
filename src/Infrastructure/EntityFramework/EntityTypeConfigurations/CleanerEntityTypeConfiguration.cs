using Domain.Aggregates.CleanerAggregate;
using Domain.Aggregates.CleanerAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.EntityTypeConfigurations;

internal class CleanerEntityTypeConfiguration : IEntityTypeConfiguration<Cleaner>
{
    public void Configure(EntityTypeBuilder<Cleaner> builder)
    {
        builder.ToTable(nameof(Cleaner), nameof(Cleaner));
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnOrder(0);
        builder.Property(c => c.AggregateId)
            .HasColumnName($"{nameof(Cleaner)}{nameof(Cleaner.AggregateId)}")
            .HasColumnOrder(1);
        builder.HasIndex(c => c.AggregateId)
            .IsUnique();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(2000);
        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(c => c.Email)
            .HasMaxLength(255);
        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.OwnsMany(c => c.CleanerOfferedServices, offeredService =>
        {
            offeredService.ToTable(nameof(CleanerOfferedService), nameof(Cleaner));
            offeredService.WithOwner().HasForeignKey("CleanerId");
            offeredService.Property<int>("Id").ValueGeneratedOnAdd();
            offeredService.HasKey("Id");
            offeredService.Property(os => os.OfferedServiceAggregateId)
                .IsRequired();
        });

        builder.Ignore(c => c.DomainEvents);
        builder.Property<byte[]>("RowVersion").IsRowVersion();
    }
}
