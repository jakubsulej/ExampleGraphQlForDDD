using Domain.Abstractions;
using Domain.Aggregates.ServiceOfferAggregate.ValueObjects;
using Domain.Shared.Enums;
using System.Collections.ObjectModel;

namespace Domain.Aggregates.ServiceOfferAggregate;

public class ServiceOffer : AggregateRoot
{
    private readonly List<ServicePricing> _servicePricings = [];

    public Guid CleanerAggregateId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    public ReadOnlyCollection<ServicePricing> ServicePricings => _servicePricings.AsReadOnly();

    private ServiceOffer() { }

    public static ServiceOffer Create(
        Guid aggregateId,
        Guid cleanerAggregateId,
        string title,
        string description,
        List<ServicePricing> servicePricings)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentException("Aggregate ID cannot be empty", nameof(aggregateId));
        if (cleanerAggregateId == Guid.Empty)
            throw new ArgumentException("Cleaner aggregate ID cannot be empty", nameof(cleanerAggregateId));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required", nameof(title));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required", nameof(description));
        if (servicePricings == null || servicePricings.Count == 0)
            throw new ArgumentException("At least one service pricing is required", nameof(servicePricings));

        var serviceOffer = new ServiceOffer
        {
            AggregateId = aggregateId,
            CleanerAggregateId = cleanerAggregateId,
            Title = title.Trim(),
            Description = description.Trim(),
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        serviceOffer._servicePricings.AddRange(servicePricings);

        // TODO: Register domain event: ServiceOfferCreatedEvent

        return serviceOffer;
    }

    public void UpdateDetails(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required", nameof(title));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required", nameof(description));

        Title = title.Trim();
        Description = description.Trim();
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: ServiceOfferDetailsUpdatedEvent
    }

    public void AddPricing(ServicePricing pricing)
    {
        if (pricing == null)
            throw new ArgumentNullException(nameof(pricing));

        if (_servicePricings.Any(p => p.PricingModel == pricing.PricingModel))
            throw new InvalidOperationException($"Pricing for {pricing.PricingModel} already exists");

        _servicePricings.Add(pricing);
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: PricingAddedToServiceOfferEvent
    }

    public void UpdatePricing(PricingModel pricingModel, ServicePricing newPricing)
    {
        if (newPricing == null)
            throw new ArgumentNullException(nameof(newPricing));

        var existingPricing = _servicePricings.FirstOrDefault(p => p.PricingModel == pricingModel);
        if (existingPricing == null)
            throw new InvalidOperationException($"Pricing for {pricingModel} does not exist");

        _servicePricings.Remove(existingPricing);
        _servicePricings.Add(newPricing);
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: PricingUpdatedInServiceOfferEvent
    }

    public void RemovePricing(PricingModel pricingModel)
    {
        if (_servicePricings.Count <= 1)
            throw new InvalidOperationException("Cannot remove the last pricing option");

        var pricingToRemove = _servicePricings.FirstOrDefault(p => p.PricingModel == pricingModel);
        if (pricingToRemove == null)
            throw new InvalidOperationException($"Pricing for {pricingModel} does not exist");

        _servicePricings.Remove(pricingToRemove);
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: PricingRemovedFromServiceOfferEvent
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: ServiceOfferActivatedEvent
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: ServiceOfferDeactivatedEvent
    }

    public void Archive()
    {
        if (IsArchived)
            return;

        IsArchived = true;
        ArchivedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: ServiceOfferArchivedEvent
    }
}
