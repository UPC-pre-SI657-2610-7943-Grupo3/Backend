namespace HomeLink.InCleanHome.API.Profiles.Domain.Model.ValueObjects;

/// <summary>
///     Address with geocoordinates obtained from Google Maps Platform.
/// </summary>
public record GeoAddress(string Street, string District, string City, double Latitude, double Longitude)
{
    public GeoAddress() : this(string.Empty, string.Empty, "Lima", 0, 0) { }

    public string FullAddress => $"{Street}, {District}, {City}";
}
