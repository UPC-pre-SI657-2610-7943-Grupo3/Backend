using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Commands;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Transform;

public static class CreateAvailabilitySlotCommandFromResourceAssembler
{
    public static CreateAvailabilitySlotCommand ToCommandFromResource(CreateAvailabilitySlotResource resource)
        => new(resource.WorkerProfileId, resource.DayOfWeek,
               TimeOnly.Parse(resource.StartTime), TimeOnly.Parse(resource.EndTime));
}
