using HomeLink.InCleanHome.API.IAM.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.IAM.Domain.Repositories;

/// <summary>
///     Repository contract for the <see cref="User"/> aggregate.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email);
    bool ExistsByEmail(string email);
}
