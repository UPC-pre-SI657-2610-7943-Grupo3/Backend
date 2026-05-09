using HomeLink.InCleanHome.API.Profiles.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.ValueObjects;

namespace HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;

/// <summary>
///     Worker profile aggregate root.
/// </summary>
/// <remarks>
///     A worker offers domestic services. The profile is created in
///     <see cref="VerificationStatus.Pending"/> state and waits for admin approval
///     before becoming visible in the SearchAndCatalog bounded context.
/// </remarks>
public partial class WorkerProfile
{
    public int Id { get; }
    public int UserId { get; private set; }
    public PersonName Name { get; private set; } = new();
    public PhoneNumber Phone { get; private set; } = new();
    public GeoAddress Address { get; private set; } = new();

    public string Biography { get; private set; } = string.Empty;
    public string Experience { get; private set; } = string.Empty;
    public decimal HourlyRate { get; private set; }
    public decimal AverageRating { get; private set; }
    public int CompletedServices { get; private set; }
    public string VerificationStatus { get; private set; } = ValueObjects.VerificationStatus.Pending;

    public string FullName => Name.FullName;
    public string PhoneNumber => Phone.Number;
    public string FullAddress => Address.FullAddress;

    public WorkerProfile() { }

    public WorkerProfile(CreateWorkerProfileCommand command)
    {
        UserId = command.UserId;
        Name = new PersonName(command.FirstName, command.LastName);
        Phone = new PhoneNumber(command.PhoneNumber);
        Address = new GeoAddress(command.Street, command.District, command.City, command.Latitude, command.Longitude);
        Biography = command.Biography;
        Experience = command.Experience;
        HourlyRate = command.HourlyRate;
        AverageRating = 0;
        CompletedServices = 0;
        VerificationStatus = ValueObjects.VerificationStatus.Pending;
    }

    public WorkerProfile UpdateProfileInfo(string biography, string experience, decimal hourlyRate)
    {
        Biography = biography;
        Experience = experience;
        HourlyRate = hourlyRate;
        return this;
    }

    public WorkerProfile UpdateAddress(string street, string district, string city, double latitude, double longitude)
    {
        Address = new GeoAddress(street, district, city, latitude, longitude);
        return this;
    }

    public WorkerProfile Approve()
    {
        VerificationStatus = ValueObjects.VerificationStatus.Approved;
        return this;
    }

    public WorkerProfile Reject()
    {
        VerificationStatus = ValueObjects.VerificationStatus.Rejected;
        return this;
    }

    public WorkerProfile RegisterCompletedService(decimal newRating)
    {
        var totalRatings = AverageRating * CompletedServices;
        CompletedServices += 1;
        AverageRating = (totalRatings + newRating) / CompletedServices;
        return this;
    }
}
