using Domain.Aggregates.PaymentAggregate;
using Domain.Aggregates.PaymentAggregate.Enums;

namespace Domain.Tests.Aggregates;

public class PaymentTests
{
    [Fact]
    public void Create_Should_Throw_When_Amount_NonPositive()
    {
        var act = () => Payment.Create(Guid.NewGuid(), Guid.NewGuid(), 0, PaymentMethod.Cash);
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Process_Should_Set_Status_Processing()
    {
        var payment = CreatePayment();

        payment.Process("txn-1");

        Assert.Equal(PaymentStatus.Processing, payment.Status);
        Assert.Equal("txn-1", payment.TransactionId);
    }

    [Fact]
    public void Complete_Should_Set_Status_Completed()
    {
        var payment = CreatePayment();
        payment.Process("txn-1");

        payment.Complete("txn-1");

        Assert.Equal(PaymentStatus.Completed, payment.Status);
        Assert.NotNull(payment.PaidAt);
    }

    [Fact]
    public void Fail_Should_Set_Status_Failed()
    {
        var payment = CreatePayment();

        payment.Fail("card declined");

        Assert.Equal(PaymentStatus.Failed, payment.Status);
    }

    private static Payment CreatePayment() =>
        Payment.Create(Guid.NewGuid(), Guid.NewGuid(), 100, PaymentMethod.CreditCard);
}

