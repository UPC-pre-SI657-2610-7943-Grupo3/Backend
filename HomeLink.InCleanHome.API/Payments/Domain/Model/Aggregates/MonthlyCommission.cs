using HomeLink.InCleanHome.API.Payments.Domain.Model.ValueObjects;

namespace HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;

/// <summary>
///     Monthly commission aggregate (10% applied to a worker's monthly earnings).
/// </summary>
/// <remarks>
///     Implements requirement US-34/US-35: "10% commission per service realized" billed
///     to the worker by the platform.
/// </remarks>
public partial class MonthlyCommission
{
    public const decimal CommissionRate = 0.10m;

    public int Id { get; }
    public int WorkerId { get; private set; }
    public int Year { get; private set; }
    public int Month { get; private set; }
    public int TotalServices { get; private set; }
    public decimal TotalEarnings { get; private set; }
    public decimal CommissionAmount { get; private set; }
    public string Status { get; private set; } = CommissionStatus.Pending;

    public MonthlyCommission() { }

    public MonthlyCommission(int workerId, int year, int month, int totalServices, decimal totalEarnings)
    {
        WorkerId = workerId;
        Year = year;
        Month = month;
        TotalServices = totalServices;
        TotalEarnings = totalEarnings;
        CommissionAmount = Math.Round(totalEarnings * CommissionRate, 2);
        Status = CommissionStatus.Pending;
    }

    public MonthlyCommission MarkAsPaid()
    {
        Status = CommissionStatus.Paid;
        return this;
    }

    public MonthlyCommission MarkAsOverdue()
    {
        Status = CommissionStatus.Overdue;
        return this;
    }
}
