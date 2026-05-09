using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Profiles.Domain.Repositories;
using HomeLink.InCleanHome.API.Profiles.Domain.Services;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.Profiles.Application.Internal.CommandServices;

public class ClientProfileCommandService(
    IClientProfileRepository clientProfileRepository,
    IUnitOfWork unitOfWork) : IClientProfileCommandService
{
    public async Task<ClientProfile?> Handle(CreateClientProfileCommand command)
    {
        if (clientProfileRepository.ExistsByUserId(command.UserId))
            return null;

        var profile = new ClientProfile(command);
        try
        {
            await clientProfileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
