namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Resources;

public record CreateReviewResource(
    int BookingRequestId,
    int ClientId,
    int WorkerId,
    int Rating,
    string Comment);
