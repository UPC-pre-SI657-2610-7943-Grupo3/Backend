using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Queries;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Application.Internal.QueryServices;

public class ServiceCategoryQueryService(IServiceCategoryRepository repository) : IServiceCategoryQueryService
{
    public async Task<IEnumerable<ServiceCategory>> Handle(GetAllServiceCategoriesQuery query)
        => await repository.ListAsync();
}
