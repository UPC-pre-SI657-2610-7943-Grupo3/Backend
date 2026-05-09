using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;

public interface IWorkerServiceRepository : IBaseRepository<WorkerService>
{
    Task<IEnumerable<WorkerService>> ListByWorkerIdAsync(int workerProfileId);
    Task<IEnumerable<WorkerService>> ListByCategoryAsync(int serviceCategoryId);
    Task<IEnumerable<WorkerService>> SearchAsync(int? categoryId, decimal? maxPrice);
}
