using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Transform;

public static class WorkerProfileResourceFromEntityAssembler
{
    public static WorkerProfileResource ToResourceFromEntity(WorkerProfile entity)
        => new(entity.Id, entity.UserId, entity.FullName, entity.PhoneNumber, entity.FullAddress,
               entity.Biography, entity.Experience, entity.HourlyRate, entity.AverageRating,
               entity.CompletedServices, entity.VerificationStatus);
}
