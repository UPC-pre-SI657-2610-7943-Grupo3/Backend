namespace HomeLink.InCleanHome.API.IAM.Interfaces.ACL;

/// <summary>
///     Anti-Corruption Layer (ACL) facade exposing IAM operations to other bounded contexts.
/// </summary>
public interface IIamContextFacade
{
    Task<int> CreateUser(string email, string password, string role);
    Task<int> FetchUserIdByEmail(string email);
    Task<string> FetchEmailByUserId(int userId);
    Task<string> FetchRoleByUserId(int userId);
}
