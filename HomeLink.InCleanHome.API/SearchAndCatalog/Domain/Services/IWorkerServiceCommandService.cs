using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Commands;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;

public interface IWorkerServiceCommandService
{
    Task<WorkerService?> Handle(CreateWorkerServiceCommand command);
}
