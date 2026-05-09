using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Queries;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Services;

public interface IProfileReportQueryService
{
    Task<IEnumerable<ProfileReport>> Handle(GetProfileReportsQuery query);
}
