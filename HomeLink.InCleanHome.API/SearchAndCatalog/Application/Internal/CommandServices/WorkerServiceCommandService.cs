using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Commands;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Application.Internal.CommandServices;

public class WorkerServiceCommandService(
    IWorkerServiceRepository repository,
    IUnitOfWork unitOfWork) : IWorkerServiceCommandService
{
    public async Task<WorkerService?> Handle(CreateWorkerServiceCommand command)
    {
        var workerService = new WorkerService(command);
        await repository.AddAsync(workerService);
        await unitOfWork.CompleteAsync();
        return workerService;
    }
}
