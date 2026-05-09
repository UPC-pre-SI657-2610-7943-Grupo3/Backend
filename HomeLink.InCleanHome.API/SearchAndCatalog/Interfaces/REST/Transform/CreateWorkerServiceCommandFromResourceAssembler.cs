using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Commands;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Transform;

public static class CreateWorkerServiceCommandFromResourceAssembler
{
    public static CreateWorkerServiceCommand ToCommandFromResource(CreateWorkerServiceResource resource)
        => new(resource.WorkerProfileId, resource.ServiceCategoryId, resource.Title, resource.Description, resource.Price);
}
