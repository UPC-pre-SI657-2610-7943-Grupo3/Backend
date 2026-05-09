namespace HomeLink.InCleanHome.API.Payments.Interfaces.REST.Resources;

public record CalculateMonthlyCommissionResource(
    int WorkerId,
    int Year,
    int Month,
    int TotalServices,
    decimal TotalEarnings);
