namespace HomeLink.InCleanHome.API.Booking.Domain.Model.Commands;

public record CancelBookingCommand(int BookingRequestId, string CancelledBy);
