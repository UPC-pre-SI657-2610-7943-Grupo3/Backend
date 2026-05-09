namespace HomeLink.InCleanHome.API.Profiles.Interfaces.REST.Resources;

public record ClientProfileResource(
    int Id,
    int UserId,
    string FullName,
    string PhoneNumber,
    string FullAddress);
