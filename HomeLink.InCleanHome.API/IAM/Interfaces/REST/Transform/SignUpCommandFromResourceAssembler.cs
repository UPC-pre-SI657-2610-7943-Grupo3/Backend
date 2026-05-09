using HomeLink.InCleanHome.API.IAM.Domain.Model.Commands;
using HomeLink.InCleanHome.API.IAM.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
        => new(resource.Email, resource.Password, resource.Role);
}
