using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.Payments.Domain.Repositories;

public interface IMonthlyCommissionRepository : IBaseRepository<MonthlyCommission>
{
    Task<IEnumerable<MonthlyCommission>> ListByWorkerIdAsync(int workerId);
    Task<MonthlyCommission?> FindByPeriodAsync(int workerId, int year, int month);
}
