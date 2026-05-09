using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Queries;
using HomeLink.InCleanHome.API.Profiles.Domain.Repositories;
using HomeLink.InCleanHome.API.Profiles.Domain.Services;

namespace HomeLink.InCleanHome.API.Profiles.Application.Internal.QueryServices;

public class WorkerProfileQueryService(IWorkerProfileRepository repository) : IWorkerProfileQueryService
{
    public async Task<WorkerProfile?> Handle(GetWorkerProfileByIdQuery query)
        => await repository.FindByIdAsync(query.WorkerProfileId);

    public async Task<IEnumerable<WorkerProfile>> Handle(GetAllWorkerProfilesQuery query)
        => await repository.ListAsync();
}
