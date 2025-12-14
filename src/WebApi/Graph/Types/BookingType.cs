using Domain.Aggregates.BookingAggregate.ReadModels;

namespace WebApi.Graph.Types;

public sealed class BookingType : ObjectType<BookingReadModel>
{
    protected override void Configure(IObjectTypeDescriptor<BookingReadModel> d)
    {
        base.Configure(d);
    }
}