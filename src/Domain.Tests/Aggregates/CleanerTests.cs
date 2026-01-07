using Domain.Aggregates.CleanerAggregate;

namespace Domain.Tests.Aggregates;

public class CleanerTests
{
    [Fact]
    public void Create_Should_Require_Name()
    {
        var act = () => Cleaner.Create(Guid.NewGuid(), "", "desc", "123");
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void AddOfferedService_Should_Add_Once()
    {
        var cleaner = CreateCleaner();
        var serviceId = Guid.NewGuid();

        cleaner.AddOfferedService(serviceId);

        Assert.Single(cleaner.CleanerOfferedServices);
    }

    [Fact]
    public void AddOfferedService_Should_Throw_When_Duplicate()
    {
        var cleaner = CreateCleaner();
        var serviceId = Guid.NewGuid();
        cleaner.AddOfferedService(serviceId);

        var act = () => cleaner.AddOfferedService(serviceId);

        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void RemoveOfferedService_Should_Remove()
    {
        var cleaner = CreateCleaner();
        var serviceId = Guid.NewGuid();
        cleaner.AddOfferedService(serviceId);

        cleaner.RemoveOfferedService(serviceId);

        Assert.Empty(cleaner.CleanerOfferedServices);
    }

    [Fact]
    public void RemoveOfferedService_Should_Throw_When_NotFound()
    {
        var cleaner = CreateCleaner();
        var act = () => cleaner.RemoveOfferedService(Guid.NewGuid());
        Assert.Throws<InvalidOperationException>(act);
    }

    private static Cleaner CreateCleaner() =>
        Cleaner.Create(Guid.NewGuid(), "Name", "Description", "+1-555-1000", "email@example.com");
}

