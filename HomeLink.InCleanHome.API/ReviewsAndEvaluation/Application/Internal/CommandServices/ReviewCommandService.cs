using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Commands;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Repositories;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Services;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Application.Internal.CommandServices;

public class ReviewCommandService(
    IReviewRepository repository,
    IUnitOfWork unitOfWork) : IReviewCommandService
{
    public async Task<Review?> Handle(CreateReviewCommand command)
    {
        if (repository.ExistsByBookingId(command.BookingRequestId)) return null;

        var review = new Review(command);
        await repository.AddAsync(review);
        await unitOfWork.CompleteAsync();
        return review;
    }
}
