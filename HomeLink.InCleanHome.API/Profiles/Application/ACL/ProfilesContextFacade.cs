using HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Queries;
using HomeLink.InCleanHome.API.Profiles.Domain.Services;
using HomeLink.InCleanHome.API.Profiles.Interfaces.ACL;

namespace HomeLink.InCleanHome.API.Profiles.Application.ACL;

public class ProfilesContextFacade(
    IClientProfileCommandService clientCommandService,
    IClientProfileQueryService clientQueryService,
    IWorkerProfileCommandService workerCommandService,
    IWorkerProfileQueryService workerQueryService) : IProfilesContextFacade
{
    public async Task<int> CreateClientProfile(int userId, string firstName, string lastName, string phoneNumber,
        string street, string district, string city, double latitude, double longitude)
    {
        var command = new CreateClientProfileCommand(userId, firstName, lastName, phoneNumber,
            street, district, city, latitude, longitude);
        var result = await clientCommandService.Handle(command);
        return result?.Id ?? 0;
    }

    public async Task<int> CreateWorkerProfile(int userId, string firstName, string lastName, string phoneNumber,
        string street, string district, string city, double latitude, double longitude,
        string biography, string experience, decimal hourlyRate)
    {
        var command = new CreateWorkerProfileCommand(userId, firstName, lastName, phoneNumber,
            street, district, city, latitude, longitude, biography, experience, hourlyRate);
        var result = await workerCommandService.Handle(command);
        return result?.Id ?? 0;
    }

    public async Task<int> FetchClientProfileIdByUserId(int userId)
    {
        var profile = await clientQueryService.Handle(new GetClientProfileByUserIdQuery(userId));
        return profile?.Id ?? 0;
    }

    public async Task<int> FetchWorkerProfileIdByUserId(int userId)
    {
        // Use the repository through the query service via custom approach: not exposed here, so use ACL light query.
        // For simplicity we expose this via a dedicated query later. Returning 0 if not found.
        var query = new GetAllWorkerProfilesQuery();
        var all = await workerQueryService.Handle(query);
        var profile = all.FirstOrDefault(w => w.UserId == userId);
        return profile?.Id ?? 0;
    }
}
