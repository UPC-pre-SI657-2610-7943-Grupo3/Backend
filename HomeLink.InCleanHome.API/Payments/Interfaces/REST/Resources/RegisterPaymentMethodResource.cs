namespace HomeLink.InCleanHome.API.Payments.Interfaces.REST.Resources;

public record RegisterPaymentMethodResource(int UserId, string Type, string Reference);
