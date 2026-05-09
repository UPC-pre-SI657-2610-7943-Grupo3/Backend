using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Queries;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;

public interface IServiceCategoryQueryService
{
    Task<IEnumerable<ServiceCategory>> Handle(GetAllServiceCategoriesQuery query);
}
