using Domain.Aggregates.CustomerAggregate.ReadModels;

namespace WebApi.Graph.Types;

public sealed class CustomerType : ObjectType<CustomerReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<CustomerReadModel> d)
    {
        base.Configure(d);
    }
}
