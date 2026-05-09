namespace HomeLink.InCleanHome.API.IAM.Domain.Model.Commands;

/// <summary>
///     Command to sign in an existing user.
/// </summary>
public record SignInCommand(string Email, string Password);
