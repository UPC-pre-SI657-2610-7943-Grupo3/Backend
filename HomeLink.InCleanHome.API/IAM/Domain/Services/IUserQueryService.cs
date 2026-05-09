using HomeLink.InCleanHome.API.IAM.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.IAM.Domain.Model.Queries;

namespace HomeLink.InCleanHome.API.IAM.Domain.Services;

/// <summary>
///     User query service contract.
/// </summary>
public interface IUserQueryService
{
    Task<User?> Handle(GetUserByIdQuery query);
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByEmailQuery query);
}
