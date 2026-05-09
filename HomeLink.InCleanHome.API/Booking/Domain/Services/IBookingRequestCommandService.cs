using HomeLink.InCleanHome.API.Booking.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Booking.Domain.Model.Commands;

namespace HomeLink.InCleanHome.API.Booking.Domain.Services;

public interface IBookingRequestCommandService
{
    Task<BookingRequest?> Handle(CreateBookingRequestCommand command);
    Task<BookingRequest?> Handle(AcceptBookingCommand command);
    Task<BookingRequest?> Handle(RejectBookingCommand command);
    Task<BookingRequest?> Handle(RescheduleBookingCommand command);
    Task<BookingRequest?> Handle(CancelBookingCommand command);
    Task<BookingRequest?> Handle(CompleteBookingCommand command);
}
