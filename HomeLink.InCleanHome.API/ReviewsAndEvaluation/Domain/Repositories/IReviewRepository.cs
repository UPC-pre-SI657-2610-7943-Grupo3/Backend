using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Repositories;

public interface IReviewRepository : IBaseRepository<Review>
{
    Task<IEnumerable<Review>> ListByWorkerIdAsync(int workerId);
    Task<IEnumerable<Review>> ListByClientIdAsync(int clientId);
    bool ExistsByBookingId(int bookingRequestId);
}
