namespace HomeLink.InCleanHome.API.IAM.Application.Internal.OutboundServices;

/// <summary>
///     Outbound port for password hashing/verification.
/// </summary>
public interface IHashingService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}
