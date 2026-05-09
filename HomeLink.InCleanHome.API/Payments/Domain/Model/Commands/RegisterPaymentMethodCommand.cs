namespace HomeLink.InCleanHome.API.Payments.Domain.Model.Commands;

public record RegisterPaymentMethodCommand(int UserId, string Type, string Reference);
