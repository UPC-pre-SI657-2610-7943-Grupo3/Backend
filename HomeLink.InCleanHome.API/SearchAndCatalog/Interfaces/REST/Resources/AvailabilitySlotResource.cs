namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;

public record AvailabilitySlotResource(
    int Id,
    int WorkerProfileId,
    int DayOfWeek,
    string StartTime,
    string EndTime,
    bool IsAvailable);
