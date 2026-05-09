namespace HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;

public record CreateClientProfileCommand(
    int UserId,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Street,
    string District,
    string City,
    double Latitude,
    double Longitude);
