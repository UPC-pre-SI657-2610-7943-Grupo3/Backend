using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Queries;
using HomeLink.InCleanHome.API.Profiles.Domain.Repositories;
using HomeLink.InCleanHome.API.Profiles.Domain.Services;

namespace HomeLink.InCleanHome.API.Profiles.Application.Internal.QueryServices;

public class ClientProfileQueryService(IClientProfileRepository repository) : IClientProfileQueryService
{
    public async Task<ClientProfile?> Handle(GetClientProfileByUserIdQuery query)
        => await repository.FindByUserIdAsync(query.UserId);
}
