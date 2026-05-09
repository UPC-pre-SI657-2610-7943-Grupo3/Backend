using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Payments.Domain.Repositories;
using HomeLink.InCleanHome.API.Payments.Domain.Services;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.Payments.Application.Internal.CommandServices;

public class MonthlyCommissionCommandService(
    IMonthlyCommissionRepository repository,
    IUnitOfWork unitOfWork) : IMonthlyCommissionCommandService
{
    public async Task<MonthlyCommission?> Handle(CalculateMonthlyCommissionCommand command)
    {
        var existing = await repository.FindByPeriodAsync(command.WorkerId, command.Year, command.Month);
        if (existing is not null) return existing;

        var commission = new MonthlyCommission(command.WorkerId, command.Year, command.Month,
            command.TotalServices, command.TotalEarnings);
        await repository.AddAsync(commission);
        await unitOfWork.CompleteAsync();
        return commission;
    }

    public async Task<MonthlyCommission?> Handle(MarkCommissionPaidCommand command)
    {
        var commission = await repository.FindByIdAsync(command.CommissionId);
        if (commission is null) return null;

        commission.MarkAsPaid();
        repository.Update(commission);
        await unitOfWork.CompleteAsync();
        return commission;
    }
}
