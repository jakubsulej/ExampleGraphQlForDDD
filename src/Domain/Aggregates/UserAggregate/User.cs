using Domain.Abstractions;
using Domain.Aggregates.UserAggregate.Enums;

namespace Domain.Aggregates.UserAggregate;

public class User : AggregateRoot
{
    public string Email { get; private set; } = string.Empty;
    public UserRole UserRole { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;
    public bool IsEmailVerified { get; private set; }
    public DateTimeOffset? LastLoginAt { get; private set; }

    // Private constructor for EF Core
    private User() { }

    // Factory method for creating new users
    public static User Create(
        Guid aggregateId,
        string email,
        string passwordHash,
        UserRole userRole)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentException("Aggregate ID cannot be empty", nameof(aggregateId));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required", nameof(passwordHash));
        if (userRole == UserRole.Unspecified)
            throw new ArgumentException("User role must be specified", nameof(userRole));

        if (!IsValidEmail(email))
            throw new ArgumentException("Invalid email format", nameof(email));

        var user = new User
        {
            AggregateId = aggregateId,
            Email = email.Trim().ToLowerInvariant(),
            PasswordHash = passwordHash,
            UserRole = userRole,
            IsEmailVerified = false,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        // TODO: Register domain event: UserCreatedEvent

        return user;
    }

    // Domain methods
    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new ArgumentException("Email is required", nameof(newEmail));
        if (!IsValidEmail(newEmail))
            throw new ArgumentException("Invalid email format", nameof(newEmail));

        Email = newEmail.Trim().ToLowerInvariant();
        IsEmailVerified = false; // Require re-verification
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: UserEmailUpdatedEvent
    }

    public void UpdatePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash is required", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: UserPasswordUpdatedEvent
    }

    public void ChangeRole(UserRole newRole)
    {
        if (newRole == UserRole.Unspecified)
            throw new ArgumentException("User role must be specified", nameof(newRole));

        UserRole = newRole;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: UserRoleChangedEvent
    }

    public void VerifyEmail()
    {
        if (IsEmailVerified)
            return;

        IsEmailVerified = true;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: UserEmailVerifiedEvent
    }

    public void RecordLogin()
    {
        LastLoginAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: UserLoggedInEvent
    }

    public void Archive()
    {
        if (IsArchived)
            return;

        IsArchived = true;
        ArchivedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;

        // TODO: Register domain event: UserArchivedEvent
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
