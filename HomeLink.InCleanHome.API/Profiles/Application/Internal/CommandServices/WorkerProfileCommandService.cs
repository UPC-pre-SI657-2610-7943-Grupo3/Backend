using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Profiles.Domain.Repositories;
using HomeLink.InCleanHome.API.Profiles.Domain.Services;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.Profiles.Application.Internal.CommandServices;

public class WorkerProfileCommandService(
    IWorkerProfileRepository workerProfileRepository,
    IUnitOfWork unitOfWork) : IWorkerProfileCommandService
{
    public async Task<WorkerProfile?> Handle(CreateWorkerProfileCommand command)
    {
        if (workerProfileRepository.ExistsByUserId(command.UserId))
            return null;

        var profile = new WorkerProfile(command);
        try
        {
            await workerProfileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<WorkerProfile?> Handle(UpdateWorkerProfileCommand command)
    {
        var profile = await workerProfileRepository.FindByIdAsync(command.WorkerProfileId);
        if (profile is null) return null;

        profile.UpdateProfileInfo(command.Biography, command.Experience, command.HourlyRate);
        workerProfileRepository.Update(profile);
        await unitOfWork.CompleteAsync();
        return profile;
    }

    public async Task<WorkerProfile?> Handle(ApproveWorkerProfileCommand command)
    {
        var profile = await workerProfileRepository.FindByIdAsync(command.WorkerProfileId);
        if (profile is null) return null;

        profile.Approve();
        workerProfileRepository.Update(profile);
        await unitOfWork.CompleteAsync();
        return profile;
    }
}
