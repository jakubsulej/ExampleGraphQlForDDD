using Domain.Aggregates.CleanerAggregate.ReadModels;

namespace WebApi.Graph.Types;

public sealed class CleanerType : ObjectType<CleanerReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<CleanerReadModel> d)
    {
        base.Configure(d);
    }
}
