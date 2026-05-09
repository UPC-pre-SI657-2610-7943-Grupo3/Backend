using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Commands;
using HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Transform;

public static class CreateServiceCategoryCommandFromResourceAssembler
{
    public static CreateServiceCategoryCommand ToCommandFromResource(CreateServiceCategoryResource resource)
        => new(resource.Name, resource.Description);
}
