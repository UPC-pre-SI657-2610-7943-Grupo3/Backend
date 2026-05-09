using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.ValueObjects;
using HomeLink.InCleanHome.API.Profiles.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLink.InCleanHome.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class WorkerProfileRepository(AppDbContext context)
    : BaseRepository<WorkerProfile>(context), IWorkerProfileRepository
{
    public async Task<WorkerProfile?> FindByUserIdAsync(int userId)
        => await Context.Set<WorkerProfile>().FirstOrDefaultAsync(p => p.UserId == userId);

    public bool ExistsByUserId(int userId)
        => Context.Set<WorkerProfile>().Any(p => p.UserId == userId);

    public async Task<IEnumerable<WorkerProfile>> ListApprovedAsync()
        => await Context.Set<WorkerProfile>()
            .Where(w => w.VerificationStatus == VerificationStatus.Approved)
            .ToListAsync();

    public async Task<IEnumerable<WorkerProfile>> ListByDistrictAsync(string district)
        => await Context.Set<WorkerProfile>()
            .Where(w => w.VerificationStatus == VerificationStatus.Approved &&
                        w.Address.District == district)
            .ToListAsync();
}
