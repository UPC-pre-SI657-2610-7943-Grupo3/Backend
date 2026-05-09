using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Infrastructure.Persistence.EFC.Repositories;

public class AvailabilitySlotRepository(AppDbContext context)
    : BaseRepository<AvailabilitySlot>(context), IAvailabilitySlotRepository
{
    public async Task<IEnumerable<AvailabilitySlot>> ListByWorkerIdAsync(int workerProfileId)
        => await Context.Set<AvailabilitySlot>()
            .Where(a => a.WorkerProfileId == workerProfileId).ToListAsync();

    public async Task<IEnumerable<AvailabilitySlot>> ListByDayOfWeekAsync(int dayOfWeek)
        => await Context.Set<AvailabilitySlot>()
            .Where(a => a.DayOfWeek == dayOfWeek && a.IsAvailable).ToListAsync();
}
