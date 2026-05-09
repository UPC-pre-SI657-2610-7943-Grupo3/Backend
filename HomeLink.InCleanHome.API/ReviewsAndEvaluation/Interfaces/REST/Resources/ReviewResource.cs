namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Resources;

public record ReviewResource(
    int Id,
    int BookingRequestId,
    int ClientId,
    int WorkerId,
    int Rating,
    string Comment);
