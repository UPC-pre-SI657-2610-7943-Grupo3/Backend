using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Transform;

public static class AvailabilitySlotResourceFromEntityAssembler
{
    public static AvailabilitySlotResource ToResourceFromEntity(AvailabilitySlot entity)
        => new(entity.Id, entity.WorkerProfileId, entity.DayOfWeek,
               entity.StartTime.ToString("HH:mm"), entity.EndTime.ToString("HH:mm"), entity.IsAvailable);
}
