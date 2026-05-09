using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Queries;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;

public interface IWorkerServiceQueryService
{
    Task<IEnumerable<WorkerService>> Handle(GetWorkerServicesByWorkerIdQuery query);
    Task<IEnumerable<WorkerService>> Handle(SearchWorkerServicesQuery query);
}
