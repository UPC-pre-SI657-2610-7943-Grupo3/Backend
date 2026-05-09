using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.Profiles.Domain.Repositories;

public interface IWorkerProfileRepository : IBaseRepository<WorkerProfile>
{
    Task<WorkerProfile?> FindByUserIdAsync(int userId);
    bool ExistsByUserId(int userId);
    Task<IEnumerable<WorkerProfile>> ListApprovedAsync();
    Task<IEnumerable<WorkerProfile>> ListByDistrictAsync(string district);
}
