namespace HomeLink.InCleanHome.API.Profiles.Domain.Model.ValueObjects;

public record PhoneNumber(string Number)
{
    public PhoneNumber() : this(string.Empty) { }
}
