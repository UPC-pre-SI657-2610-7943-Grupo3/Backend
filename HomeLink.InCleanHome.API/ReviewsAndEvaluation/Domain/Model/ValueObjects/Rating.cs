namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.ValueObjects;

/// <summary>
///     A 1-to-5 star rating value object.
/// </summary>
public record Rating
{
    public int Value { get; }

    public Rating(int value)
    {
        if (value < 1 || value > 5)
            throw new ArgumentException("Rating must be between 1 and 5.", nameof(value));
        Value = value;
    }

    public static implicit operator int(Rating r) => r.Value;
}
