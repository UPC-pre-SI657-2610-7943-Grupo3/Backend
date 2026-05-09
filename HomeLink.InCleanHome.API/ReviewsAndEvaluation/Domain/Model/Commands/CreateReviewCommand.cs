namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Commands;

public record CreateReviewCommand(
    int BookingRequestId,
    int ClientId,
    int WorkerId,
    int Rating,
    string Comment);
