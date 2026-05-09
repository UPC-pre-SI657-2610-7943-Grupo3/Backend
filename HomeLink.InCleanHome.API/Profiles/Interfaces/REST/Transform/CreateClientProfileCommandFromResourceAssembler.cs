using HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Transform;

public static class CreateClientProfileCommandFromResourceAssembler
{
    public static CreateClientProfileCommand ToCommandFromResource(CreateClientProfileResource resource)
        => new(resource.UserId, resource.FirstName, resource.LastName, resource.PhoneNumber,
               resource.Street, resource.District, resource.City, resource.Latitude, resource.Longitude);
}
