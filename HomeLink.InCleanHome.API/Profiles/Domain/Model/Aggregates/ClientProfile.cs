using HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.ValueObjects;

namespace HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;

/// <summary>
///     Client profile aggregate root for the Profiles bounded context.
/// </summary>
/// <remarks>
///     A client is a household / professional / family hiring a domestic service.
/// </remarks>
public partial class ClientProfile
{
    public int Id { get; }
    public int UserId { get; private set; }
    public PersonName Name { get; private set; } = new();
    public PhoneNumber Phone { get; private set; } = new();
    public GeoAddress Address { get; private set; } = new();

    public string FullName => Name.FullName;
    public string PhoneNumber => Phone.Number;
    public string FullAddress => Address.FullAddress;

    public ClientProfile() { }

    public ClientProfile(CreateClientProfileCommand command)
    {
        UserId = command.UserId;
        Name = new PersonName(command.FirstName, command.LastName);
        Phone = new PhoneNumber(command.PhoneNumber);
        Address = new GeoAddress(command.Street, command.District, command.City, command.Latitude, command.Longitude);
    }

    public ClientProfile UpdateContactInfo(string firstName, string lastName, string phoneNumber)
    {
        Name = new PersonName(firstName, lastName);
        Phone = new PhoneNumber(phoneNumber);
        return this;
    }

    public ClientProfile UpdateAddress(string street, string district, string city, double latitude, double longitude)
    {
        Address = new GeoAddress(street, district, city, latitude, longitude);
        return this;
    }
}
