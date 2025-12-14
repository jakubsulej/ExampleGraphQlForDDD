using Domain.Shared.Enums;

namespace Domain.Aggregates.BookingAggregate.ReadModels;

public class ServicePricingSnapshotReadModel
{
    public int Price { get; set; }
    public PricingModel PricingModel { get; set; }
}
