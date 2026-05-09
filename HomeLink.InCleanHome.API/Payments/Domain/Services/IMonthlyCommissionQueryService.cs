using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Queries;

namespace HomeLink.InCleanHome.API.Payments.Domain.Services;

public interface IMonthlyCommissionQueryService
{
    Task<IEnumerable<MonthlyCommission>> Handle(GetCommissionsByWorkerIdQuery query);
    Task<MonthlyCommission?> Handle(GetCommissionByPeriodQuery query);
}
