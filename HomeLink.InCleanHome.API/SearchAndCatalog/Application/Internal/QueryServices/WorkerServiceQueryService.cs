using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Queries;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Application.Internal.QueryServices;

public class WorkerServiceQueryService(IWorkerServiceRepository repository) : IWorkerServiceQueryService
{
    public async Task<IEnumerable<WorkerService>> Handle(GetWorkerServicesByWorkerIdQuery query)
        => await repository.ListByWorkerIdAsync(query.WorkerProfileId);

    public async Task<IEnumerable<WorkerService>> Handle(SearchWorkerServicesQuery query)
        => await repository.SearchAsync(query.ServiceCategoryId, query.MaxPrice);
}
