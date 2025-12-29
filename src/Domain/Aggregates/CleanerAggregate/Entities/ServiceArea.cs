using Domain.Abstractions;

namespace Domain.Aggregates.CleanerAggregate.Entities;

public class ServiceArea : Entity
{
    public int CleanerId { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public int MaxAllowedDistance { get; set; }
}
