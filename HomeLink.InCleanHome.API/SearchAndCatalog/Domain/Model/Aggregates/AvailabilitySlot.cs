namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;

/// <summary>
///     A weekly availability slot of a worker (used for filtering by availability).
/// </summary>
public class AvailabilitySlot
{
    public int Id { get; }
    public int WorkerProfileId { get; private set; }
    public int DayOfWeek { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public AvailabilitySlot() { }

    public AvailabilitySlot(int workerProfileId, int dayOfWeek, TimeOnly start, TimeOnly end)
    {
        WorkerProfileId = workerProfileId;
        DayOfWeek = dayOfWeek;
        StartTime = start;
        EndTime = end;
        IsAvailable = true;
    }

    public AvailabilitySlot UpdateRange(TimeOnly start, TimeOnly end)
    {
        StartTime = start;
        EndTime = end;
        return this;
    }

    public AvailabilitySlot SetAvailability(bool available)
    {
        IsAvailable = available;
        return this;
    }
}
