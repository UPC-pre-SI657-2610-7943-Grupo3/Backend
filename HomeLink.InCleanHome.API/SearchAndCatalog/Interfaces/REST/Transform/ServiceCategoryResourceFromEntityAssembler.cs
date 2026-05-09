using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Transform;

public static class ServiceCategoryResourceFromEntityAssembler
{
    public static ServiceCategoryResource ToResourceFromEntity(ServiceCategory entity)
        => new(entity.Id, entity.Name, entity.Description);
}
