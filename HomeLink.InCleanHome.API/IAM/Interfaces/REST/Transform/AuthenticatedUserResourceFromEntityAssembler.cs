using HomeLink.InCleanHome.API.IAM.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.IAM.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
        => new(user.Id, user.Email, user.Role, user.IsVerified, token);
}
