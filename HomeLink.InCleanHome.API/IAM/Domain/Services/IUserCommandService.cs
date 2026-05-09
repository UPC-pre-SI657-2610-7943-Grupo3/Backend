using HomeLink.InCleanHome.API.IAM.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.IAM.Domain.Model.Commands;

namespace HomeLink.InCleanHome.API.IAM.Domain.Services;

/// <summary>
///     User command service contract.
/// </summary>
public interface IUserCommandService
{
    /// <summary>Authenticates a user. Returns the user and a JWT token.</summary>
    Task<(User user, string token)> Handle(SignInCommand command);

    /// <summary>Registers a new user. Workers stay unverified until admin approves.</summary>
    Task Handle(SignUpCommand command);

    /// <summary>Activates (verifies) a worker account.</summary>
    Task Handle(VerifyUserCommand command);
}
