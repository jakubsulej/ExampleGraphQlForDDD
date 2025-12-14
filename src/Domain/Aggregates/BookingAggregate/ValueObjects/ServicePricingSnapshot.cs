using Domain.Abstractions;
using Domain.Shared.Enums;

namespace Domain.Aggregates.BookingAggregate.ValueObjects;

public class ServicePricingSnapshot : ValueObject
{
    public int Price { get; set; }
    public PricingModel PricingModel { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
    }
}
