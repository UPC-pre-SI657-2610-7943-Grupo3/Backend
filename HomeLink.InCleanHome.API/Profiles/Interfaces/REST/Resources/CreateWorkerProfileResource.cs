namespace HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Resources;

public record CreateWorkerProfileResource(
    int UserId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Street,
    string District,
    string City,
    double Latitude,
    double Longitude,
    string Biography,
    string Experience,
    decimal HourlyRate);
