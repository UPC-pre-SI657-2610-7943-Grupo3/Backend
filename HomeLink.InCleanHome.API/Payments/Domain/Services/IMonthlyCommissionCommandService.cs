using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Commands;

namespace HomeLink.InCleanHome.API.Payments.Domain.Services;

public interface IMonthlyCommissionCommandService
{
    Task<MonthlyCommission?> Handle(CalculateMonthlyCommissionCommand command);
    Task<MonthlyCommission?> Handle(MarkCommissionPaidCommand command);
}
