using Domain.Aggregates.BookingAggregate.ReadModels;
using WebApi.Graph.Loaders;

namespace WebApi.Graph.Types;

public sealed class BookingType : ObjectType<BookingReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<BookingReadModel> d)
    {
        base.Configure(d);
        
        d.Field(o => o.Customer)
            .Type<ObjectType<Domain.Aggregates.CustomerAggregate.ReadModels.CustomerReadModel>>()
            .Resolve(async ctx =>
            {
                var booking = ctx.Parent<BookingReadModel>();
                var loader = ctx.DataLoader<CustomerByAggregateIdDataLoader>();
                var customers = await loader.LoadAsync(booking.CustomerAggregateId, ctx.RequestAborted);
                return customers?.FirstOrDefault();
            });
        
        d.Field(o => o.ServiceOffer)
            .Type<ObjectType<Domain.Aggregates.ServiceOfferAggregate.ReadModels.ServiceOfferReadModel>>()
            .Resolve(async ctx =>
            {
                var booking = ctx.Parent<BookingReadModel>();
                var loader = ctx.DataLoader<ServiceOfferByAggregateIdDataLoader>();
                var serviceOffers = await loader.LoadAsync(booking.ServiceOfferAggregateId, ctx.RequestAborted);
                return serviceOffers?.FirstOrDefault();
            });
        
        d.Field(o => o.ServicePricingSnapshots)
            .Type<ListType<ObjectType<ServicePricingSnapshotReadModel>>>();
        
        d.Field(o => o.BookingReviews)
            .Type<ListType<ObjectType<BookingReviewReadModel>>>();
    }
}