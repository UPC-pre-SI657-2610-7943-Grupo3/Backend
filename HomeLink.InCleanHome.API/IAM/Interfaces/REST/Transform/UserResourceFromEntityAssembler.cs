using HomeLink.InCleanHome.API.IAM.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.IAM.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
        => new(user.Id, user.Email, user.Role, user.IsVerified);
}
