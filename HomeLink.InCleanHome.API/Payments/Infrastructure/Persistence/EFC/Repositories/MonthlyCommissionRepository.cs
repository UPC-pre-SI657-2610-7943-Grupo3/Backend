using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLink.InCleanHome.API.Payments.Infrastructure.Persistence.EFC.Repositories;

public class MonthlyCommissionRepository(AppDbContext context)
    : BaseRepository<MonthlyCommission>(context), IMonthlyCommissionRepository
{
    public async Task<IEnumerable<MonthlyCommission>> ListByWorkerIdAsync(int workerId)
        => await Context.Set<MonthlyCommission>()
            .Where(c => c.WorkerId == workerId).ToListAsync();

    public async Task<MonthlyCommission?> FindByPeriodAsync(int workerId, int year, int month)
        => await Context.Set<MonthlyCommission>()
            .FirstOrDefaultAsync(c => c.WorkerId == workerId && c.Year == year && c.Month == month);
}
