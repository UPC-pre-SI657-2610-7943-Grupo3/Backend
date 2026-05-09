using HomeLink.InCleanHome.API.IAM.Domain.Model.Aggregates;

namespace HomeLink.InCleanHome.API.IAM.Application.Internal.OutboundServices;

/// <summary>
///     Outbound port for issuing/validating JWT tokens.
/// </summary>
public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}
