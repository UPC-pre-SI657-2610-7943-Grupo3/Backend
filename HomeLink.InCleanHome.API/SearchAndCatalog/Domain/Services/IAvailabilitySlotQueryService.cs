using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Queries;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Services;

public interface IAvailabilitySlotQueryService
{
    Task<IEnumerable<AvailabilitySlot>> Handle(GetAvailabilityByWorkerIdQuery query);
}
