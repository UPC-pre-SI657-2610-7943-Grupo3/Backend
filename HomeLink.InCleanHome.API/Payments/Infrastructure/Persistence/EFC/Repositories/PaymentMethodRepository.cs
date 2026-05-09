using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLink.InCleanHome.API.Payments.Infrastructure.Persistence.EFC.Repositories;

public class PaymentMethodRepository(AppDbContext context)
    : BaseRepository<PaymentMethod>(context), IPaymentMethodRepository
{
    public async Task<IEnumerable<PaymentMethod>> ListByUserIdAsync(int userId)
        => await Context.Set<PaymentMethod>()
            .Where(p => p.UserId == userId && p.IsActive).ToListAsync();
}
