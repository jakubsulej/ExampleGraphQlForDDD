using Domain.Aggregates.BookingReviewAggregate.ReadModels;

namespace WebApi.Graph.Types;

public sealed class BookingReviewType : ObjectType<BookingReviewReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<BookingReviewReadModel> d)
    {
        base.Configure(d);
    }
}
