using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;

public interface IServiceCategoryRepository : IBaseRepository<ServiceCategory>
{
    bool ExistsByName(string name);
}
