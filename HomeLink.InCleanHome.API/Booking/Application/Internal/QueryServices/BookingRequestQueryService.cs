using HomeLink.InCleanHome.API.Booking.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Booking.Domain.Model.Queries;
using HomeLink.InCleanHome.API.Booking.Domain.Repositories;
using HomeLink.InCleanHome.API.Booking.Domain.Services;

namespace HomeLink.InCleanHome.API.Booking.Application.Internal.QueryServices;

public class BookingRequestQueryService(IBookingRequestRepository repository) : IBookingRequestQueryService
{
    public async Task<BookingRequest?> Handle(GetBookingByIdQuery query)
        => await repository.FindByIdAsync(query.BookingRequestId);

    public async Task<IEnumerable<BookingRequest>> Handle(GetBookingsByClientQuery query)
        => await repository.ListByClientIdAsync(query.ClientId);

    public async Task<IEnumerable<BookingRequest>> Handle(GetBookingsByWorkerQuery query)
        => await repository.ListByWorkerIdAsync(query.WorkerId);
}
