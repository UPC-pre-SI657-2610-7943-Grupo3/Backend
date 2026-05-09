using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Commands;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;

/// <summary>
///     Review aggregate root.
/// </summary>
/// <remarks>
///     Implements US-09 / US-14: a client may rate (1-5) and comment on a completed booking.
/// </remarks>
public partial class Review
{
    public int Id { get; }
    public int BookingRequestId { get; private set; }
    public int ClientId { get; private set; }
    public int WorkerId { get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; } = string.Empty;

    public Review() { }

    public Review(CreateReviewCommand command)
    {
        if (command.Rating < 1 || command.Rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");

        BookingRequestId = command.BookingRequestId;
        ClientId = command.ClientId;
        WorkerId = command.WorkerId;
        Rating = command.Rating;
        Comment = command.Comment;
    }
}
