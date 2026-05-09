namespace HomeLink.InCleanHome.API.IAM.Interfaces.REST.Resources;

public record UserResource(int Id, string Email, string Role, bool IsVerified);
