using Domain.Abstractions;
using Domain.Aggregates.UserAggregate.Enums;

namespace Domain.Aggregates.UserAggregate;

public class User : AggregateRoot
{
    public string Email { get; set; }
    public UserRole UserRole { get; set; }
    public string PasswordHash { get; set; }
}
