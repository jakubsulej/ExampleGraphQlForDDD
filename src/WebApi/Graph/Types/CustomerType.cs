using Domain.Aggregates.CustomerAggregate.ReadModels;
using WebApi.Graph.Loaders;

namespace WebApi.Graph.Types;

public sealed class CustomerType : ObjectType<CustomerReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<CustomerReadModel> d)
    {
        base.Configure(d);
        
        d.Field(o => o.Bookings)
            .Type<ListType<ObjectType<Domain.Aggregates.BookingAggregate.ReadModels.BookingReadModel>>>()
            .Resolve(ctx =>
            {
                var customer = ctx.Parent<CustomerReadModel>();
                var loader = ctx.DataLoader<BookingsByCustomerAggregateIdDataLoader>();
                return loader.LoadAsync(customer.AggregateId, ctx.RequestAborted);
            });
    }
}
