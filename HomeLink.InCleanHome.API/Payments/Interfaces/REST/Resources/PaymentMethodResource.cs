namespace HomeLink.InCleanHome.API.Payments.Interfaces.REST.Resources;

public record PaymentMethodResource(int Id, int UserId, string Type, string Reference, bool IsActive);
