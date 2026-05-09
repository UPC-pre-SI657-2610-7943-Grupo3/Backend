using HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Transform;

public static class UpdateWorkerProfileCommandFromResourceAssembler
{
    public static UpdateWorkerProfileCommand ToCommandFromResource(int workerProfileId, UpdateWorkerProfileResource resource)
        => new(workerProfileId, resource.Biography, resource.Experience, resource.HourlyRate);
}
