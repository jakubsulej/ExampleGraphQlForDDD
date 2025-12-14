namespace Domain.Abstractions;

public abstract class ValueObject
{
    private int? _cachedHashCode;
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null) 
            return false;

        if (GetType() != obj.GetType()) 
            return false;   

        var other = (ValueObject)obj;

        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        if (!_cachedHashCode.HasValue)
        {
            var hashCode = new HashCode();

            foreach (var component in GetEqualityComponents()) 
                hashCode.Add(component);

            _cachedHashCode = hashCode.ToHashCode();
        }

        return _cachedHashCode.Value;
    }
}
