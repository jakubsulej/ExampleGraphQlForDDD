namespace Domain.Aggregates.PaymentAggregate.Entities;

public enum PaymentMethod
{
    Undefined = 0,
    CreditCard = 1,
    DebitCard = 2,
    PayPal = 3,
    BankTransfer = 4,
    Cash = 5
}
