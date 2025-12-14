using Domain.Aggregates.ServiceOfferAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.EntityTypeConfigurations;

internal class ServiceOfferEntityTypeConfiguration : IEntityTypeConfiguration<ServiceOffer>
{
    public void Configure(EntityTypeBuilder<ServiceOffer> builder)
    {
        throw new NotImplementedException();
    }
}
