using Domain.Aggregates.BookingAggregate.ReadModels;

namespace WebApi.Graph.Types;

public sealed class ServicePricingSnapshotType : ObjectType<ServicePricingSnapshotReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<ServicePricingSnapshotReadModel> d)
    {
        base.Configure(d);
    }
}

