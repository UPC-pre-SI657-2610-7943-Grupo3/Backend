using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;

namespace HomeLink.InCleanHome.API.Profiles.Domain.Services;

public interface IWorkerProfileCommandService
{
    Task<WorkerProfile?> Handle(CreateWorkerProfileCommand command);
    Task<WorkerProfile?> Handle(UpdateWorkerProfileCommand command);
    Task<WorkerProfile?> Handle(ApproveWorkerProfileCommand command);
}
