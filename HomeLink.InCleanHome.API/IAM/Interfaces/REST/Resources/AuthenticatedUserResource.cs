namespace HomeLink.InCleanHome.API.IAM.Interfaces.REST.Resources;

public record AuthenticatedUserResource(int Id, string Email, string Role, bool IsVerified, string Token);
