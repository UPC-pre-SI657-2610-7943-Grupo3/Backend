using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;

namespace HomeLink.InCleanHome.API.Profiles.Domain.Services;

public interface IClientProfileCommandService
{
    Task<ClientProfile?> Handle(CreateClientProfileCommand command);
}
