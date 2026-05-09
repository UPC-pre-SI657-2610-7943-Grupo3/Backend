using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Queries;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Services;

public interface IReviewQueryService
{
    Task<IEnumerable<Review>> Handle(GetReviewsByWorkerIdQuery query);
    Task<IEnumerable<Review>> Handle(GetReviewsByClientIdQuery query);
}
