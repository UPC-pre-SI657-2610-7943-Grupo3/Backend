namespace HomeLink.InCleanHome.API.Payments.Interfaces.REST.Resources;

public record MonthlyCommissionResource(
    int Id,
    int WorkerId,
    int Year,
    int Month,
    int TotalServices,
    decimal TotalEarnings,
    decimal CommissionAmount,
    string Status);
