using Domain.Abstractions;
using Domain.Aggregates.CleanerAggregate.ValueObjects;

namespace Domain.Aggregates.CleanerAggregate;

public class Cleaner : AggregateRoot
{
    private readonly List<CleanerOfferedService> _cleanerOfferedServices = [];

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<CleanerOfferedService> CleanerOfferedServices => _cleanerOfferedServices.AsReadOnly();

    private Cleaner() { }

    public static Cleaner Create(
        Guid aggregateId,
        string name,
        string description,
        string phoneNumber,
        string? email = null)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentException("Aggregate ID cannot be empty", nameof(aggregateId));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required", nameof(description));
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required", nameof(phoneNumber));

        var cleaner = new Cleaner
        {
            AggregateId = aggregateId,
            Name = name.Trim(),
            Description = description.Trim(),
            PhoneNumber = phoneNumber.Trim(),
            Email = email?.Trim(),
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        // TODO: Register domain event: CleanerCreatedEvent

        return cleaner;
    }

    public void UpdateProfile(string name, string description, string phoneNumber, string? email = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required", nameof(description));
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required", nameof(phoneNumber));

        Name = name.Trim();
        Description = description.Trim();
        PhoneNumber = phoneNumber.Trim();
        Email = email?.Trim();
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: CleanerProfileUpdatedEvent
    }

    public void AddOfferedService(Guid serviceOfferAggregateId)
    {
        if (serviceOfferAggregateId == Guid.Empty)
            throw new ArgumentException("Service offer aggregate ID cannot be empty", nameof(serviceOfferAggregateId));

        if (_cleanerOfferedServices.Any(s => s.OfferedServiceAggregateId == serviceOfferAggregateId))
            throw new InvalidOperationException("Service is already offered by this cleaner");

        var offeredService = new CleanerOfferedService(serviceOfferAggregateId);
        _cleanerOfferedServices.Add(offeredService);
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: ServiceAddedToCleanerEvent
    }

    public void RemoveOfferedService(Guid serviceOfferAggregateId)
    {
        if (serviceOfferAggregateId == Guid.Empty)
            throw new ArgumentException("Service offer aggregate ID cannot be empty", nameof(serviceOfferAggregateId));

        var serviceToRemove = _cleanerOfferedServices.FirstOrDefault(s => s.OfferedServiceAggregateId == serviceOfferAggregateId);
        if (serviceToRemove == null)
            throw new InvalidOperationException("Service is not offered by this cleaner");

        _cleanerOfferedServices.Remove(serviceToRemove);
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: ServiceRemovedFromCleanerEvent
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: CleanerActivatedEvent
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: CleanerDeactivatedEvent
    }

    public void Archive()
    {
        if (IsArchived)
            return;

        IsArchived = true;
        ArchivedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: CleanerArchivedEvent
    }
}
