using HomeLink.InCleanHome.API.Booking.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Booking.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLink.InCleanHome.API.Booking.Infrastructure.Persistence.EFC.Repositories;

public class BookingRequestRepository(AppDbContext context)
    : BaseRepository<BookingRequest>(context), IBookingRequestRepository
{
    public async Task<IEnumerable<BookingRequest>> ListByClientIdAsync(int clientId)
        => await Context.Set<BookingRequest>().Where(b => b.ClientId == clientId).ToListAsync();

    public async Task<IEnumerable<BookingRequest>> ListByWorkerIdAsync(int workerId)
        => await Context.Set<BookingRequest>().Where(b => b.WorkerId == workerId).ToListAsync();
}
