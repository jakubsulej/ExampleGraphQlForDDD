namespace Domain.Abstractions;

public abstract class Entity
{
    public long Id { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset UpdatedAt { get; protected set; }
    public DateTimeOffset ArchivedAt { get; protected set; }
    public bool IsArchived { get; protected set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != obj.GetType()) 
            return false;

        if (Id.Equals(default) || other.Id.Equals(Id)) 
            return false;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode() 
        => (GetType().ToString() + Id).GetHashCode();
}
