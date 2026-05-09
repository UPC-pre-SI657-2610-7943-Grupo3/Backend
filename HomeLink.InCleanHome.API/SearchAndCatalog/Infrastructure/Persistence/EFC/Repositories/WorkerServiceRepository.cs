using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Infrastructure.Persistence.EFC.Repositories;

public class WorkerServiceRepository(AppDbContext context)
    : BaseRepository<WorkerService>(context), IWorkerServiceRepository
{
    public async Task<IEnumerable<WorkerService>> ListByWorkerIdAsync(int workerProfileId)
        => await Context.Set<WorkerService>()
            .Where(s => s.WorkerProfileId == workerProfileId).ToListAsync();

    public async Task<IEnumerable<WorkerService>> ListByCategoryAsync(int serviceCategoryId)
        => await Context.Set<WorkerService>()
            .Where(s => s.ServiceCategoryId == serviceCategoryId && s.IsActive).ToListAsync();

    public async Task<IEnumerable<WorkerService>> SearchAsync(int? categoryId, decimal? maxPrice)
    {
        var q = Context.Set<WorkerService>().Where(s => s.IsActive);
        if (categoryId.HasValue) q = q.Where(s => s.ServiceCategoryId == categoryId.Value);
        if (maxPrice.HasValue) q = q.Where(s => s.Price <= maxPrice.Value);
        return await q.ToListAsync();
    }
}
