using HomeLink.InCleanHome.API.Booking.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Booking.Domain.Model.Queries;

namespace HomeLink.InCleanHome.API.Booking.Domain.Services;

public interface IBookingRequestQueryService
{
    Task<BookingRequest?> Handle(GetBookingByIdQuery query);
    Task<IEnumerable<BookingRequest>> Handle(GetBookingsByClientQuery query);
    Task<IEnumerable<BookingRequest>> Handle(GetBookingsByWorkerQuery query);
}
