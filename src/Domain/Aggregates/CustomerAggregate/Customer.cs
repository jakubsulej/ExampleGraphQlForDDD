using Domain.Abstractions;

namespace Domain.Aggregates.CustomerAggregate;

public class Customer : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? PhoneNumber { get; private set; }
    public string? Address { get; private set; }
    public bool IsActive { get; private set; }

    private Customer() { }

    public static Customer Create(
        Guid aggregateId,
        string name,
        string email,
        string? phoneNumber = null,
        string? address = null)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentException("Aggregate ID cannot be empty", nameof(aggregateId));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));
        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format", nameof(email));

        var customer = new Customer
        {
            AggregateId = aggregateId,
            Name = name.Trim(),
            Email = email.Trim().ToLowerInvariant(),
            PhoneNumber = phoneNumber?.Trim(),
            Address = address?.Trim(),
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        // TODO: Register domain event: CustomerCreatedEvent

        return customer;
    }

    public void UpdateProfile(string name, string email, string? phoneNumber = null, string? address = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));
        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format", nameof(email));

        Name = name.Trim();
        Email = email.Trim().ToLowerInvariant();
        PhoneNumber = phoneNumber?.Trim();
        Address = address?.Trim();
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: CustomerProfileUpdatedEvent
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: CustomerActivatedEvent
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: CustomerDeactivatedEvent
    }

    public void Archive()
    {
        if (IsArchived)
            return;

        IsArchived = true;
        ArchivedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: CustomerArchivedEvent
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
