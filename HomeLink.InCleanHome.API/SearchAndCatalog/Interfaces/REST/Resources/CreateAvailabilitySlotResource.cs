namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;

public record CreateAvailabilitySlotResource(
    int WorkerProfileId,
    int DayOfWeek,
    string StartTime,
    string EndTime);
