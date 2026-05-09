namespace HomeLink.InCleanHome.API.Booking.Interfaces.REST.Resources;

public record CreateBookingRequestResource(
    int ClientId,
    int WorkerId,
    int WorkerServiceId,
    DateTime ScheduledAt,
    decimal AgreedPrice,
    string Notes);
