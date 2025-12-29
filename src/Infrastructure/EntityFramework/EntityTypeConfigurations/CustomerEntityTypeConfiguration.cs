using Domain.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.EntityTypeConfigurations;

internal class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnOrder(0);
        builder.Property(c => c.AggregateId)
            .HasColumnName($"{nameof(Customer)}{nameof(Customer.AggregateId)}")
            .HasColumnOrder(1);
        builder.HasIndex(c => c.AggregateId)
            .IsUnique();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);
        builder.HasIndex(c => c.Email);
        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(20);
        builder.Property(c => c.Address)
            .HasMaxLength(500);
        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.Ignore(c => c.DomainEvents);
        builder.Property<byte[]>("RowVersion").IsRowVersion();
    }
}
