using HomeLink.InCleanHome.API.IAM.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.IAM.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLink.InCleanHome.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context)
    : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email)
        => await Context.Set<User>().FirstOrDefaultAsync(user => user.Email.Equals(email));

    public bool ExistsByEmail(string email)
        => Context.Set<User>().Any(user => user.Email.Equals(email));
}
