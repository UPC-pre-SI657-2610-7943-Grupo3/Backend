namespace HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;

public record UpdateWorkerProfileCommand(
    int WorkerProfileId,
    string Biography,
    string Experience,
    decimal HourlyRate);
