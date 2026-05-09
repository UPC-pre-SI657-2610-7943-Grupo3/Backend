namespace HomeLink.InCleanHome.API.Booking.Domain.Model.Commands;

public record RescheduleBookingCommand(int BookingRequestId, DateTime NewScheduledAt);
