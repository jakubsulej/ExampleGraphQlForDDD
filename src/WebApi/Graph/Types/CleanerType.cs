using Domain.Aggregates.CleanerAggregate.ReadModels;
using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using WebApi.Graph.Loaders;

namespace WebApi.Graph.Types;

public sealed class CleanerType : ObjectType<CleanerReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<CleanerReadModel> d)
    {
        base.Configure(d);
        d.Field(o => o.OfferedServices)
            .Type<ListType<ObjectType<ServiceOfferReadModel>>>()
            .Resolve(ctx =>
            {
                var cleaner = ctx.Parent<CleanerReadModel>();
                var loader = ctx.DataLoader<ServiceOffersByCleanerIdDataLoader>();
                return loader.LoadAsync(cleaner.CleanerAggregateId, ctx.RequestAborted);
            });
    }
}
