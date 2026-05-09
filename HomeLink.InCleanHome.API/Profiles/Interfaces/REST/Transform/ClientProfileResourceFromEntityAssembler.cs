using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Transform;

public static class ClientProfileResourceFromEntityAssembler
{
    public static ClientProfileResource ToResourceFromEntity(ClientProfile entity)
        => new(entity.Id, entity.UserId, entity.FullName, entity.PhoneNumber, entity.FullAddress);
}
