using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Queries;

namespace HomeLink.InCleanHome.API.Profiles.Domain.Services;

public interface IClientProfileQueryService
{
    Task<ClientProfile?> Handle(GetClientProfileByUserIdQuery query);
}
