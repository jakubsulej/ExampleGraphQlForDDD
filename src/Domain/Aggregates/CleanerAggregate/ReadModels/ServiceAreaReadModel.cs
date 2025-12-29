namespace Domain.Aggregates.CleanerAggregate.ReadModels;

public record ServiceAreaReadModel
{
    public required int CleanerId { get; init; }
    public required string City { get; init; }
    public required string PostalCode { get; init; }
    public required int MaxAllowedDistance { get; init; }
}
