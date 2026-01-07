using Domain.Aggregates.CustomerAggregate;

namespace Domain.Tests.Aggregates;

public class CustomerTests
{
    [Fact]
    public void Create_Should_Validate_Email()
    {
        var act = () => Customer.Create(Guid.NewGuid(), "Name", "not-an-email");
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void UpdateProfile_Should_Change_Fields()
    {
        var customer = Customer.Create(Guid.NewGuid(), "Name", "user@example.com");

        customer.UpdateProfile("New", "new@example.com", "123", "addr");

        Assert.Equal("New", customer.Name);
        Assert.Equal("new@example.com", customer.Email);
        Assert.Equal("123", customer.PhoneNumber);
        Assert.Equal("addr", customer.Address);
    }
}

