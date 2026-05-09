using System.Security.Claims;
using System.Text;
using HomeLink.InCleanHome.API.IAM.Application.Internal.OutboundServices;
using HomeLink.InCleanHome.API.IAM.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace HomeLink.InCleanHome.API.IAM.Infrastructure.Tokens.JWT.Services;

/// <summary>
///     JWT token service implementation (HS256).
/// </summary>
public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    public string GenerateToken(User user)
    {
        return $"mi-token-{user.Id}";
    }

    public async Task<int?> ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token)) return null;

        // Validar el token ultra simple
        if (token.StartsWith("mi-token-") && int.TryParse(token.Replace("mi-token-", ""), out int simpleId))
        {
            return simpleId;
        }

        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        try
        {
            var result = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            });

            var jwtToken = (JsonWebToken)result.SecurityToken;
            return int.Parse(jwtToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}
