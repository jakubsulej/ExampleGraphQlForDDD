using Domain.Aggregates.ServiceOfferAggregate;
using Domain.Aggregates.ServiceOfferAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.EntityTypeConfigurations;

internal class ServiceOfferEntityTypeConfiguration : IEntityTypeConfiguration<ServiceOffer>
{
    public void Configure(EntityTypeBuilder<ServiceOffer> builder)
    {
        builder.ToTable(nameof(ServiceOffer), nameof(ServiceOffer));
        builder.HasKey(so => so.Id);
        builder.Property(so => so.Id).HasColumnOrder(0);
        builder.Property(so => so.AggregateId)
            .HasColumnName($"{nameof(ServiceOffer)}{nameof(ServiceOffer.AggregateId)}")
            .HasColumnOrder(1);
        builder.HasIndex(so => so.AggregateId)
            .IsUnique();

        builder.Property(so => so.CleanerAggregateId)
            .IsRequired();
        builder.Property(so => so.Title)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(so => so.Description)
            .IsRequired()
            .HasMaxLength(2000);
        builder.Property(so => so.IsActive)
            .IsRequired();

        builder.OwnsMany(so => so.ServicePricings, pricing =>
        {
            pricing.ToTable(nameof(ServicePricing), nameof(ServiceOffer));
            pricing.WithOwner().HasForeignKey("ServiceOfferId");
            pricing.Property<int>("Id").ValueGeneratedOnAdd();
            pricing.HasKey("Id");
            pricing.Property(p => p.Price)
                .HasColumnName("Price")
                .IsRequired();
            pricing.Property(p => p.PricingModel)
                .HasColumnName("PricingModel")
                .HasConversion<string>()
                .IsRequired();
        });

        builder.Ignore(so => so.DomainEvents);
        builder.Property<byte[]>("RowVersion").IsRowVersion();
    }
}
