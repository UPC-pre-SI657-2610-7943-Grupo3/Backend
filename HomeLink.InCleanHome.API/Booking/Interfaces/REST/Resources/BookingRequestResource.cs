namespace HomeLink.InCleanHome.API.Booking.Interfaces.REST.Resources;

public record BookingRequestResource(
    int Id,
    int ClientId,
    int WorkerId,
    int WorkerServiceId,
    DateTime ScheduledAt,
    string Status,
    decimal AgreedPrice,
    string Notes);
