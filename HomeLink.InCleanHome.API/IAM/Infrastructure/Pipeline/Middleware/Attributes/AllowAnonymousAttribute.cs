namespace HomeLink.InCleanHome.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
///     Attribute used to skip JWT authorization for a specific action.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute { }
