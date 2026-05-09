using HomeLink.InCleanHome.API.Booking.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Booking.Domain.Model.ValueObjects;

namespace HomeLink.InCleanHome.API.Booking.Domain.Model.Aggregates;

/// <summary>
///     Booking request aggregate root.
/// </summary>
/// <remarks>
///     Encapsulates the lifecycle of a domestic-service hiring transaction.
///     The cancellation rule (RFU-016 / RFU-017) is enforced inside the aggregate:
///     - clients must cancel at least 3 business days before the scheduled date,
///     - workers must cancel at least 7 business days before the scheduled date.
/// </remarks>
public partial class BookingRequest
{
    public int Id { get; }
    public int ClientId { get; private set; }
    public int WorkerId { get; private set; }
    public int WorkerServiceId { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public string Status { get; private set; } = BookingStatus.Pending;
    public string Notes { get; private set; } = string.Empty;
    public decimal AgreedPrice { get; private set; }

    public BookingRequest() { }

    public BookingRequest(CreateBookingRequestCommand command)
    {
        ClientId = command.ClientId;
        WorkerId = command.WorkerId;
        WorkerServiceId = command.WorkerServiceId;
        ScheduledAt = command.ScheduledAt;
        AgreedPrice = command.AgreedPrice;
        Notes = command.Notes;
        Status = BookingStatus.Pending;
    }

    public BookingRequest Accept()
    {
        if (Status != BookingStatus.Pending && Status != BookingStatus.Rescheduled)
            throw new InvalidOperationException("Only pending or rescheduled bookings can be accepted.");
        Status = BookingStatus.Accepted;
        return this;
    }

    public BookingRequest Reject()
    {
        if (Status != BookingStatus.Pending)
            throw new InvalidOperationException("Only pending bookings can be rejected.");
        Status = BookingStatus.Rejected;
        return this;
    }

    public BookingRequest Reschedule(DateTime newScheduledAt)
    {
        if (Status is BookingStatus.Cancelled or BookingStatus.Completed or BookingStatus.Rejected)
            throw new InvalidOperationException("Booking cannot be rescheduled in its current status.");
        ScheduledAt = newScheduledAt;
        Status = BookingStatus.Rescheduled;
        return this;
    }

    public BookingRequest CancelByClient()
    {
        var daysAhead = (ScheduledAt.Date - DateTime.UtcNow.Date).TotalDays;
        if (daysAhead < 3)
            throw new InvalidOperationException("Clients must cancel at least 3 business days in advance.");
        Status = BookingStatus.Cancelled;
        return this;
    }

    public BookingRequest CancelByWorker()
    {
        var daysAhead = (ScheduledAt.Date - DateTime.UtcNow.Date).TotalDays;
        if (daysAhead < 7)
            throw new InvalidOperationException("Workers must cancel at least 7 business days in advance.");
        Status = BookingStatus.Cancelled;
        return this;
    }

    public BookingRequest Complete()
    {
        if (Status != BookingStatus.Accepted && Status != BookingStatus.Rescheduled)
            throw new InvalidOperationException("Only accepted bookings can be completed.");
        Status = BookingStatus.Completed;
        return this;
    }
}
