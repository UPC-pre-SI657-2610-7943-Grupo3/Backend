using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.Profiles.Domain.Repositories;

public interface IClientProfileRepository : IBaseRepository<ClientProfile>
{
    Task<ClientProfile?> FindByUserIdAsync(int userId);
    bool ExistsByUserId(int userId);
}
