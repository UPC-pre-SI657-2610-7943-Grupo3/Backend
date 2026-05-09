namespace HomeLink.InCleanHome.API.Booking.Domain.Model.ValueObjects;

/// <summary>
///     Booking lifecycle states.
/// </summary>
public static class BookingStatus
{
    public const string Pending = "PENDING";
    public const string Accepted = "ACCEPTED";
    public const string Rejected = "REJECTED";
    public const string Rescheduled = "RESCHEDULED";
    public const string Cancelled = "CANCELLED";
    public const string Completed = "COMPLETED";
}
