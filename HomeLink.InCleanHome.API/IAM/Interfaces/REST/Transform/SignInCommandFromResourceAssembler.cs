using HomeLink.InCleanHome.API.IAM.Domain.Model.Commands;
using HomeLink.InCleanHome.API.IAM.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
        => new(resource.Email, resource.Password);
}
