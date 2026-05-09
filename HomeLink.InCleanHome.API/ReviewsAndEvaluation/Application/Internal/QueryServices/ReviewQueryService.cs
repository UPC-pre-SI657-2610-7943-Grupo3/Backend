using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Queries;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Repositories;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Services;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Application.Internal.QueryServices;

public class ReviewQueryService(IReviewRepository repository) : IReviewQueryService
{
    public async Task<IEnumerable<Review>> Handle(GetReviewsByWorkerIdQuery query)
        => await repository.ListByWorkerIdAsync(query.WorkerId);

    public async Task<IEnumerable<Review>> Handle(GetReviewsByClientIdQuery query)
        => await repository.ListByClientIdAsync(query.ClientId);
}
