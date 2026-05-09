namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Commands;

public record CreateAvailabilitySlotCommand(
    int WorkerProfileId,
    int DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime);
