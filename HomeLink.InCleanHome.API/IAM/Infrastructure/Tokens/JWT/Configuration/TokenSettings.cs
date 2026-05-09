namespace HomeLink.InCleanHome.API.IAM.Infrastructure.Tokens.JWT.Configuration;

/// <summary>
///     Bound to the <c>TokenSettings</c> section of <c>appsettings.json</c>.
/// </summary>
public class TokenSettings
{
    public string Secret { get; set; } = string.Empty;
}
