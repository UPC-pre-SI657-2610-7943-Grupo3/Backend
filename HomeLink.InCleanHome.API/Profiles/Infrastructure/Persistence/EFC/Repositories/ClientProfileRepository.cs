using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLink.InCleanHome.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ClientProfileRepository(AppDbContext context)
    : BaseRepository<ClientProfile>(context), IClientProfileRepository
{
    public async Task<ClientProfile?> FindByUserIdAsync(int userId)
        => await Context.Set<ClientProfile>().FirstOrDefaultAsync(p => p.UserId == userId);

    public bool ExistsByUserId(int userId)
        => Context.Set<ClientProfile>().Any(p => p.UserId == userId);
}
