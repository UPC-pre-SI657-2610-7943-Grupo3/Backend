using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Queries;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Application.Internal.QueryServices;

public class AvailabilitySlotQueryService(IAvailabilitySlotRepository repository) : IAvailabilitySlotQueryService
{
    public async Task<IEnumerable<AvailabilitySlot>> Handle(GetAvailabilityByWorkerIdQuery query)
        => await repository.ListByWorkerIdAsync(query.WorkerProfileId);
}
