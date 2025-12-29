using Domain.Abstractions;
using Domain.Shared.Enums;

namespace Domain.Aggregates.ServiceOfferAggregate.ValueObjects;

public class ServicePricing : ValueObject
{
    public long Price { get; private init; }
    public PricingModel PricingModel { get; private init; }

    private ServicePricing() { }

    public static ServicePricing Create(long price, PricingModel pricingModel)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(price));
        if (pricingModel == PricingModel.Undefined)
            throw new ArgumentException("Pricing model must be specified", nameof(pricingModel));

        return new ServicePricing
        {
            Price = price,
            PricingModel = pricingModel
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
        yield return PricingModel;
    }
}
