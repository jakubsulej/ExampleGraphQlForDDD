using Domain.Aggregates.ServiceOfferAggregate.ReadModels;

namespace WebApi.Graph.Types;

public sealed class ServicePricingType : ObjectType<ServicePricingReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<ServicePricingReadModel> d)
    {
        base.Configure(d);
    }
}

