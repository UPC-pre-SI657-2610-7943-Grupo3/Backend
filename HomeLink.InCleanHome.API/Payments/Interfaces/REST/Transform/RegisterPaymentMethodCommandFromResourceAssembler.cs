using HomeLink.InCleanHome.API.Payments.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Payments.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.Payments.Interfaces.REST.Transform;

public static class RegisterPaymentMethodCommandFromResourceAssembler
{
    public static RegisterPaymentMethodCommand ToCommandFromResource(RegisterPaymentMethodResource resource)
        => new(resource.UserId, resource.Type, resource.Reference);
}
