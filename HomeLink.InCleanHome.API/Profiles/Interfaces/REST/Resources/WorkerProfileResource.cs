namespace HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Resources;

public record WorkerProfileResource(
    int Id,
    int UserId,
    string FullName,
    string PhoneNumber,
    string FullAddress,
    string Biography,
    string Experience,
    decimal HourlyRate,
    decimal AverageRating,
    int CompletedServices,
    string VerificationStatus);
