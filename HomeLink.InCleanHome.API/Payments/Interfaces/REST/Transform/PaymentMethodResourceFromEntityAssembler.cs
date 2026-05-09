using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.Payments.Interfaces.REST.Transform;

public static class PaymentMethodResourceFromEntityAssembler
{
    public static PaymentMethodResource ToResourceFromEntity(PaymentMethod entity)
        => new(entity.Id, entity.UserId, entity.Type, entity.Reference, entity.IsActive);
}
