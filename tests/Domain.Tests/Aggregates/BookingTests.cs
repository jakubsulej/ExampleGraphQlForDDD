using Domain.Aggregates.BookingAggregate;
using Domain.Aggregates.BookingAggregate.Enums;
using Domain.Aggregates.BookingAggregate.ValueObjects;
using Domain.Shared.Enums;

namespace Domain.Tests.Aggregates;

public class BookingTests
{
    [Fact]
    public void Create_Should_Throw_When_NoPricingSnapshots()
    {
        var act = () => Booking.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTimeOffset.UtcNow.AddDays(1),
            new List<ServicePricingSnapshot>());

        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Create_Should_Throw_When_ScheduledDate_InPast()
    {
        var pricing = ServicePricingSnapshot.Create(1000, PricingModel.Fixed);

        var act = () => Booking.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTimeOffset.UtcNow.AddMinutes(-1),
            new List<ServicePricingSnapshot> { pricing });

        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Confirm_Should_Set_Status_Confirmed()
    {
        var booking = CreateBooking();

        booking.Confirm();

        Assert.Equal(BookingStatus.Confirmed, booking.Status);
    }

    [Fact]
    public void Complete_Should_Work_From_Confirmed()
    {
        var booking = CreateBooking();
        booking.Confirm();

        booking.Complete();

        Assert.Equal(BookingStatus.Completed, booking.Status);
        Assert.NotNull(booking.CompletedDate);
    }

    [Fact]
    public void Cancel_Should_Set_Status_Cancelled()
    {
        var booking = CreateBooking();

        booking.Cancel("customer request");

        Assert.Equal(BookingStatus.Cancelled, booking.Status);
    }

    [Fact]
    public void AddReview_Should_Throw_When_NotCompleted()
    {
        var booking = CreateBooking();

        var act = () => booking.AddReview(Guid.NewGuid(), "Great job", 5);

        Assert.Throws<InvalidOperationException>(act);
    }

    private static Booking CreateBooking()
    {
        var snapshot = ServicePricingSnapshot.Create(1000, PricingModel.Fixed);

        return Booking.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTimeOffset.UtcNow.AddDays(1),
            new List<ServicePricingSnapshot> { snapshot });
    }
}

