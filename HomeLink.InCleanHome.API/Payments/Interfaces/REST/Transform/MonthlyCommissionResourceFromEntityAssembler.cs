using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.Payments.Interfaces.REST.Transform;

public static class MonthlyCommissionResourceFromEntityAssembler
{
    public static MonthlyCommissionResource ToResourceFromEntity(MonthlyCommission entity)
        => new(entity.Id, entity.WorkerId, entity.Year, entity.Month,
               entity.TotalServices, entity.TotalEarnings, entity.CommissionAmount, entity.Status);
}
