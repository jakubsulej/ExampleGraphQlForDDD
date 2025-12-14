using Domain.Abstractions;
using Domain.Shared.Enums;

namespace Domain.Aggregates.ServiceOfferAggregate.ReadModels;

public class ServicePricingReadModel : EntityReadModel
{
    public long Price { get; set; }
    public PricingModel PricingModel { get; set; }
}
