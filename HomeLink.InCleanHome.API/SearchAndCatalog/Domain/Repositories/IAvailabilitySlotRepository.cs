using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;

public interface IAvailabilitySlotRepository : IBaseRepository<AvailabilitySlot>
{
    Task<IEnumerable<AvailabilitySlot>> ListByWorkerIdAsync(int workerProfileId);
    Task<IEnumerable<AvailabilitySlot>> ListByDayOfWeekAsync(int dayOfWeek);
}
