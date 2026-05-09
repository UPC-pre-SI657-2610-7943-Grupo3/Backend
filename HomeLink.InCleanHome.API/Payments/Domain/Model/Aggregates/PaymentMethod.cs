using HomeLink.InCleanHome.API.Payments.Domain.Model.Commands;

namespace HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;

/// <summary>
///     Payment method aggregate root.
/// </summary>
/// <remarks>
///     Represents the method a client agreed to use to pay the worker once the service ends.
///     Off-platform payment is the canonical case; this aggregate only records the agreement.
/// </remarks>
public partial class PaymentMethod
{
    public int Id { get; }
    public int UserId { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string Reference { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;

    public PaymentMethod() { }

    public PaymentMethod(RegisterPaymentMethodCommand command)
    {
        UserId = command.UserId;
        Type = command.Type;
        Reference = command.Reference;
        IsActive = true;
    }

    public PaymentMethod Deactivate() { IsActive = false; return this; }
}
