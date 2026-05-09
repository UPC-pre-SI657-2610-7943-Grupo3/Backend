using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Repositories;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Infrastructure.Persistence.EFC.Repositories;

public class ReviewRepository(AppDbContext context)
    : BaseRepository<Review>(context), IReviewRepository
{
    public async Task<IEnumerable<Review>> ListByWorkerIdAsync(int workerId)
        => await Context.Set<Review>().Where(r => r.WorkerId == workerId).ToListAsync();

    public async Task<IEnumerable<Review>> ListByClientIdAsync(int clientId)
        => await Context.Set<Review>().Where(r => r.ClientId == clientId).ToListAsync();

    public bool ExistsByBookingId(int bookingRequestId)
        => Context.Set<Review>().Any(r => r.BookingRequestId == bookingRequestId);
}
