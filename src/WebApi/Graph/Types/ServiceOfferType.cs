using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using WebApi.Graph.Loaders;

namespace WebApi.Graph.Types;

public sealed class ServiceOfferType : ObjectType<ServiceOfferReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<ServiceOfferReadModel> d)
    {
        base.Configure(d);
        
        d.Field(o => o.Cleaner)
            .Type<ObjectType<Domain.Aggregates.CleanerAggregate.ReadModels.CleanerReadModel>>()
            .Resolve(ctx =>
            {
                var serviceOffer = ctx.Parent<ServiceOfferReadModel>();
                var loader = ctx.DataLoader<CleanerByAggregateIdDataLoader>();
                return loader.LoadAsync(serviceOffer.CleanerAggregateId, ctx.RequestAborted);
            });
    }
}
