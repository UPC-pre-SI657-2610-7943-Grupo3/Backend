using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Transform;

public static class WorkerServiceResourceFromEntityAssembler
{
    public static WorkerServiceResource ToResourceFromEntity(WorkerService entity)
        => new(entity.Id, entity.WorkerProfileId, entity.ServiceCategoryId,
               entity.Title, entity.Description, entity.Price, entity.IsActive);
}
