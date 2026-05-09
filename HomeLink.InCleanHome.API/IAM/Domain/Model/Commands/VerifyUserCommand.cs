namespace HomeLink.InCleanHome.API.IAM.Domain.Model.Commands;

/// <summary>
///     Command to verify (activate) a worker account after admin review.
/// </summary>
public record VerifyUserCommand(int UserId);
