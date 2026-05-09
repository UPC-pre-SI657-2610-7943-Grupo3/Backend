namespace HomeLink.InCleanHome.API.Booking.Domain.Model.Commands;

public record CreateBookingRequestCommand(
    int ClientId,
    int WorkerId,
    int WorkerServiceId,
    DateTime ScheduledAt,
    decimal AgreedPrice,
    string Notes);
