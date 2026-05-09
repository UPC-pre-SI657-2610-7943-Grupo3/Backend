using HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Transform;

public static class CreateWorkerProfileCommandFromResourceAssembler
{
    public static CreateWorkerProfileCommand ToCommandFromResource(CreateWorkerProfileResource resource)
        => new(resource.UserId, resource.FirstName, resource.LastName, resource.PhoneNumber,
               resource.Street, resource.District, resource.City, resource.Latitude, resource.Longitude,
               resource.Biography, resource.Experience, resource.HourlyRate);
}
