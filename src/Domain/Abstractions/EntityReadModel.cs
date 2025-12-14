namespace Domain.Abstractions;

public class EntityReadModel
{
    public required long Id { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset UpdatedAt { get; init; }
    public required DateTimeOffset ArchivedAt { get; init; }
    public required bool IsArchived { get; init; }
}
