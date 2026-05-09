namespace HomeLink.InCleanHome.API.IAM.Domain.Model.ValueObjects;

/// <summary>
///     Roles supported by the InCleanHome platform.
/// </summary>
/// <remarks>
///     <list type="bullet">
///         <item><description>Client: a household demanding a domestic service.</description></item>
///         <item><description>Worker: a domestic worker offering services.</description></item>
///         <item><description>Admin: a back-office user that can verify worker accounts.</description></item>
///     </list>
/// </remarks>
public static class UserRole
{
    public const string Client = "CLIENT";
    public const string Worker = "WORKER";
    public const string Admin = "ADMIN";

    public static bool IsValid(string role) =>
        role is Client or Worker or Admin;
}
