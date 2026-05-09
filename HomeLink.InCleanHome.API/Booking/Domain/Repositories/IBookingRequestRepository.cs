using HomeLink.InCleanHome.API.Booking.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.Booking.Domain.Repositories;

public interface IBookingRequestRepository : IBaseRepository<BookingRequest>
{
    Task<IEnumerable<BookingRequest>> ListByClientIdAsync(int clientId);
    Task<IEnumerable<BookingRequest>> ListByWorkerIdAsync(int workerId);
}
