using Domain.Aggregates.ReviewAggregate;

namespace Domain.Tests.Aggregates;

public class ReviewTests
{
    [Fact]
    public void Create_Should_Throw_When_Rating_OutOfRange()
    {
        var act = () => Review.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "text", 0);
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Publish_Should_Set_IsPublished()
    {
        var review = CreateReview();

        review.Publish();

        Assert.True(review.IsPublished);
        Assert.NotNull(review.PublishedAt);
    }

    [Fact]
    public void Update_Should_Throw_When_Published()
    {
        var review = CreateReview();
        review.Publish();

        var act = () => review.Update("new", 4);

        Assert.Throws<InvalidOperationException>(act);
    }

    private static Review CreateReview() =>
        Review.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Nice", 5);
}

