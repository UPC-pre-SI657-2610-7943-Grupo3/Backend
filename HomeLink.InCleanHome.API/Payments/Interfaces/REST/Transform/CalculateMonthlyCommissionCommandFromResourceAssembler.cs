using HomeLink.InCleanHome.API.Payments.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Payments.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.Payments.Interfaces.REST.Transform;

public static class CalculateMonthlyCommissionCommandFromResourceAssembler
{
    public static CalculateMonthlyCommissionCommand ToCommandFromResource(CalculateMonthlyCommissionResource resource)
        => new(resource.WorkerId, resource.Year, resource.Month, resource.TotalServices, resource.TotalEarnings);
}
