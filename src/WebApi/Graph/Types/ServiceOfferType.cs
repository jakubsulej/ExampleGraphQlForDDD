using Domain.Aggregates.ServiceOfferAggregate.ReadModels;

namespace WebApi.Graph.Types;

public sealed class ServiceOfferType : ObjectType<ServiceOfferReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<ServiceOfferReadModel> d)
    {
        base.Configure(d);
    }
}
