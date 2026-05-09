using HomeLink.InCleanHome.API.IAM.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.IAM.Domain.Model.Queries;
using HomeLink.InCleanHome.API.IAM.Domain.Repositories;
using HomeLink.InCleanHome.API.IAM.Domain.Services;

namespace HomeLink.InCleanHome.API.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query)
        => await userRepository.FindByIdAsync(query.Id);

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
        => await userRepository.ListAsync();

    public async Task<User?> Handle(GetUserByEmailQuery query)
        => await userRepository.FindByEmailAsync(query.Email);
}
