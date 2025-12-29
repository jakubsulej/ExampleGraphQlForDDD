using Domain.Abstractions;
using Domain.Shared.Enums;

namespace Domain.Aggregates.BookingAggregate.ValueObjects;

public class ServicePricingSnapshot : ValueObject
{
    public long Price { get; private init; }
    public PricingModel PricingModel { get; private init; }
    public DateTimeOffset SnapshotDate { get; private init; }

    // Private constructor for EF Core
    private ServicePricingSnapshot() { }

    // Factory method for creating value objects
    public static ServicePricingSnapshot Create(long price, PricingModel pricingModel, DateTimeOffset? snapshotDate = null)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(price));
        if (pricingModel == PricingModel.Undefined)
            throw new ArgumentException("Pricing model must be specified", nameof(pricingModel));

        return new ServicePricingSnapshot
        {
            Price = price,
            PricingModel = pricingModel,
            SnapshotDate = snapshotDate ?? DateTimeOffset.UtcNow
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
        yield return PricingModel;
        yield return SnapshotDate;
    }
}
