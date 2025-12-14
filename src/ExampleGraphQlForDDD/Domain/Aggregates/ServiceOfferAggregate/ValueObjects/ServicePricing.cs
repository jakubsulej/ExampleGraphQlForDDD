using Domain.Abstractions;
using Domain.Shared.Enums;

namespace Domain.Aggregates.ServiceOfferAggregate.ValueObjects;

public class ServicePricing : ValueObject
{
    public long Price { get; set; }
    public PricingModel PricingModel { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
