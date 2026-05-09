namespace HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Resources;

public record UpdateWorkerProfileResource(
    string Biography,
    string Experience,
    decimal HourlyRate);
