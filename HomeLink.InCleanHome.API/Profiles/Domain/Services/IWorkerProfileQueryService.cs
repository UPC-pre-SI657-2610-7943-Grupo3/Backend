using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Queries;

namespace HomeLink.InCleanHome.API.Profiles.Domain.Services;

public interface IWorkerProfileQueryService
{
    Task<WorkerProfile?> Handle(GetWorkerProfileByIdQuery query);
    Task<IEnumerable<WorkerProfile>> Handle(GetAllWorkerProfilesQuery query);
}
