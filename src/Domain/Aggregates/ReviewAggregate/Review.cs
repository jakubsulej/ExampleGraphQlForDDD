using Domain.Abstractions;

namespace Domain.Aggregates.ReviewAggregate;

public class Review : AggregateRoot
{
    public required string Comment { get; set; }
    public int Rating { get; set; }
}
