using HomeLink.InCleanHome.API.IAM.Domain.Model.Commands;
using HomeLink.InCleanHome.API.IAM.Domain.Model.Queries;
using HomeLink.InCleanHome.API.IAM.Domain.Services;

namespace HomeLink.InCleanHome.API.IAM.Interfaces.ACL.Services;

public class IamContextFacade(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService) : IIamContextFacade
{
    public async Task<int> CreateUser(string email, string password, string role)
    {
        var command = new SignUpCommand(email, password, role);
        await userCommandService.Handle(command);
        var user = await userQueryService.Handle(new GetUserByEmailQuery(email));
        return user?.Id ?? 0;
    }

    public async Task<int> FetchUserIdByEmail(string email)
    {
        var user = await userQueryService.Handle(new GetUserByEmailQuery(email));
        return user?.Id ?? 0;
    }

    public async Task<string> FetchEmailByUserId(int userId)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId));
        return user?.Email ?? string.Empty;
    }

    public async Task<string> FetchRoleByUserId(int userId)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId));
        return user?.Role ?? string.Empty;
    }
}
