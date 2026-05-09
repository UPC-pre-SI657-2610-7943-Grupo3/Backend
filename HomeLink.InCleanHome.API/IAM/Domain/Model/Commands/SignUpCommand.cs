namespace HomeLink.InCleanHome.API.IAM.Domain.Model.Commands;

/// <summary>
///     Command to sign up a new user (client or worker).
/// </summary>
public record SignUpCommand(string Email, string Password, string Role);
