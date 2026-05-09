namespace HomeLink.InCleanHome.API.Profiles.Interfaces.ACL;

/// <summary>
///     Anti-Corruption Layer for the Profiles bounded context.
///     Used by Booking, Payments and Reviews contexts to obtain profile metadata.
/// </summary>
public interface IProfilesContextFacade
{
    Task<int> CreateClientProfile(int userId, string firstName, string lastName, string phoneNumber,
        string street, string district, string city, double latitude, double longitude);

    Task<int> CreateWorkerProfile(int userId, string firstName, string lastName, string phoneNumber,
        string street, string district, string city, double latitude, double longitude,
        string biography, string experience, decimal hourlyRate);

    Task<int> FetchClientProfileIdByUserId(int userId);
    Task<int> FetchWorkerProfileIdByUserId(int userId);
}
