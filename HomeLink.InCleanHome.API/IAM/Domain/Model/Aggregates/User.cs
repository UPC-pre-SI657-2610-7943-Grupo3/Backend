using System.Text.Json.Serialization;
using HomeLink.InCleanHome.API.IAM.Domain.Model.ValueObjects;

namespace HomeLink.InCleanHome.API.IAM.Domain.Model.Aggregates;

/// <summary>
///     User aggregate root for the IAM (User Management) bounded context.
/// </summary>
/// <remarks>
///     A user represents an account in the InCleanHome platform.
///     The role differentiates between clients (households) and workers (domestic workers).
///     Workers start in <c>IsVerified = false</c> state until back-office verification completes.
/// </remarks>
public class User
{
    public int Id { get; }
    public string Email { get; private set; }

    [JsonIgnore]
    public string PasswordHash { get; private set; }

    public string Role { get; private set; }
    public bool IsVerified { get; private set; }

    public User()
    {
        Email = string.Empty;
        PasswordHash = string.Empty;
        Role = UserRole.Client;
        IsVerified = false;
    }

    public User(string email, string passwordHash, string role)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = UserRole.IsValid(role) ? role : UserRole.Client;
        // Clients are auto-verified. Workers must be reviewed by admin first.
        IsVerified = role == UserRole.Client;
    }

    public User UpdateEmail(string email)
    {
        Email = email;
        return this;
    }

    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }

    public User Verify()
    {
        IsVerified = true;
        return this;
    }
}
