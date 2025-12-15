using Domain.Aggregates.CleanerAggregate;
using Domain.Aggregates.ServiceOfferAggregate.ReadModels;
using HotChocolate.Types;

namespace WebApi.Graph.Types;

public sealed class ServiceOfferType : ObjectType<ServiceOfferReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<ServiceOfferReadModel> d)
    {
        base.Configure(d);
        //d.Field(o => o.Cleaner)
        //    .Type<NonNullType<ListType<NonNullType<ObjectType<Cleaner>>>>>()
        //    .Resolve(ctx =>
        //    {
        //        var serviceOffer = ctx.Parent<ServiceOfferReadModel>();
        //        //var loader = ctx.DataLoader<>
        //    });
    }
}
