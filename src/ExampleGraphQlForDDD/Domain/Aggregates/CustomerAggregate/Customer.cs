using Domain.Abstractions;

namespace Domain.Aggregates.CustomerAggregate;

public class Customer : AggregateRoot
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}
