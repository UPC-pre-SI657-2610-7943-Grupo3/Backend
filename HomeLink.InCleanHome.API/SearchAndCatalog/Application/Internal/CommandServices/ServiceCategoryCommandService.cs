using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Commands;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Application.Internal.CommandServices;

public class ServiceCategoryCommandService(
    IServiceCategoryRepository repository,
    IUnitOfWork unitOfWork) : IServiceCategoryCommandService
{
    public async Task<ServiceCategory?> Handle(CreateServiceCategoryCommand command)
    {
        if (repository.ExistsByName(command.Name)) return null;
        var category = new ServiceCategory(command.Name, command.Description);
        await repository.AddAsync(category);
        await unitOfWork.CompleteAsync();
        return category;
    }
}
