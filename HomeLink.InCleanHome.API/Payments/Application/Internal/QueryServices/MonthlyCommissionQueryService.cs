using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Queries;
using HomeLink.InCleanHome.API.Payments.Domain.Repositories;
using HomeLink.InCleanHome.API.Payments.Domain.Services;

namespace HomeLink.InCleanHome.API.Payments.Application.Internal.QueryServices;

public class MonthlyCommissionQueryService(IMonthlyCommissionRepository repository) : IMonthlyCommissionQueryService
{
    public async Task<IEnumerable<MonthlyCommission>> Handle(GetCommissionsByWorkerIdQuery query)
        => await repository.ListByWorkerIdAsync(query.WorkerId);

    public async Task<MonthlyCommission?> Handle(GetCommissionByPeriodQuery query)
        => await repository.FindByPeriodAsync(query.WorkerId, query.Year, query.Month);
}
