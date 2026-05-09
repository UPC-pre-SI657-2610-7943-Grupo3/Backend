namespace HomeLink.InCleanHome.API.Payments.Domain.Model.Commands;

public record CalculateMonthlyCommissionCommand(
    int WorkerId,
    int Year,
    int Month,
    int TotalServices,
    decimal TotalEarnings);
