using Domain.Aggregates.ServiceOfferAggregate;
using Domain.Aggregates.ServiceOfferAggregate.ValueObjects;
using Domain.Shared.Enums;

namespace Domain.Tests.Aggregates;

public class ServiceOfferTests
{
    [Fact]
    public void Create_Should_Require_Pricing()
    {
        var act = () => ServiceOffer.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Title",
            "Description",
            new List<ServicePricing>());

        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void AddPricing_Should_Throw_When_Duplicate_Model()
    {
        var offer = CreateServiceOffer();
        var duplicate = ServicePricing.Create(2000, PricingModel.Fixed);

        var act = () => offer.AddPricing(duplicate);

        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void RemovePricing_Should_Throw_When_Last()
    {
        var offer = ServiceOffer.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Title",
            "Description",
            new List<ServicePricing> { ServicePricing.Create(1000, PricingModel.Fixed) });

        var act = () => offer.RemovePricing(PricingModel.Fixed);

        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void UpdateDetails_Should_Change_Title_And_Description()
    {
        var offer = CreateServiceOffer();

        offer.UpdateDetails("New Title", "New Desc");

        Assert.Equal("New Title", offer.Title);
        Assert.Equal("New Desc", offer.Description);
    }

    private static ServiceOffer CreateServiceOffer()
    {
        return ServiceOffer.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Title",
            "Description",
            new List<ServicePricing>
            {
                ServicePricing.Create(1000, PricingModel.Fixed),
                ServicePricing.Create(2000, PricingModel.Hourly)
            });
    }
}

