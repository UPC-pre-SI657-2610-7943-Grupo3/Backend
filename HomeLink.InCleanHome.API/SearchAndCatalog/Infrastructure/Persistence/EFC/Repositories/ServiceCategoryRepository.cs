using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Infrastructure.Persistence.EFC.Repositories;

public class ServiceCategoryRepository(AppDbContext context)
    : BaseRepository<ServiceCategory>(context), IServiceCategoryRepository
{
    public bool ExistsByName(string name) =>
        Context.Set<ServiceCategory>().Any(c => c.Name == name);
}
