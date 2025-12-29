using Domain.Aggregates.BookingAggregate.ReadModels;
using WebApi.Graph.Loaders;

namespace WebApi.Graph.Types;

public sealed class BookingReviewType : ObjectType<BookingReviewReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<BookingReviewReadModel> d)
    {
        base.Configure(d);
        
        d.Field(o => o.Booking)
            .Type<ObjectType<BookingReadModel>>()
            .Resolve(async ctx =>
            {
                var review = ctx.Parent<BookingReviewReadModel>();
                // Note: BookingId is the database ID, not aggregate ID
                // We need to get the booking by its database ID or find another way
                // For now, this will need the booking aggregate ID in the review model
                // or we need a different approach
                return null; // TODO: Implement if needed
            });
    }
}
