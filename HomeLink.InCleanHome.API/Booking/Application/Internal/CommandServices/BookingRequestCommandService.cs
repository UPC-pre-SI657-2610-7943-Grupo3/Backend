using HomeLink.InCleanHome.API.Booking.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Booking.Domain.Model.Commands;
using HomeLink.InCleanHome.API.Booking.Domain.Repositories;
using HomeLink.InCleanHome.API.Booking.Domain.Services;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.Booking.Application.Internal.CommandServices;

public class BookingRequestCommandService(
    IBookingRequestRepository repository,
    IUnitOfWork unitOfWork) : IBookingRequestCommandService
{
    public async Task<BookingRequest?> Handle(CreateBookingRequestCommand command)
    {
        var booking = new BookingRequest(command);
        await repository.AddAsync(booking);
        await unitOfWork.CompleteAsync();
        return booking;
    }

    public async Task<BookingRequest?> Handle(AcceptBookingCommand command)
        => await ApplyTransition(command.BookingRequestId, b => b.Accept());

    public async Task<BookingRequest?> Handle(RejectBookingCommand command)
        => await ApplyTransition(command.BookingRequestId, b => b.Reject());

    public async Task<BookingRequest?> Handle(RescheduleBookingCommand command)
        => await ApplyTransition(command.BookingRequestId, b => b.Reschedule(command.NewScheduledAt));

    public async Task<BookingRequest?> Handle(CancelBookingCommand command)
        => await ApplyTransition(command.BookingRequestId,
            b => command.CancelledBy.Equals("CLIENT", StringComparison.OrdinalIgnoreCase)
                ? b.CancelByClient() : b.CancelByWorker());

    public async Task<BookingRequest?> Handle(CompleteBookingCommand command)
        => await ApplyTransition(command.BookingRequestId, b => b.Complete());

    private async Task<BookingRequest?> ApplyTransition(int bookingId, Func<BookingRequest, BookingRequest> action)
    {
        var booking = await repository.FindByIdAsync(bookingId);
        if (booking is null) return null;
        action(booking);
        repository.Update(booking);
        await unitOfWork.CompleteAsync();
        return booking;
    }
}
