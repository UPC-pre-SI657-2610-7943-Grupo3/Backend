using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Queries;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Repositories;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Services;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Application.Internal.QueryServices;

public class ProfileReportQueryService(IProfileReportRepository repository) : IProfileReportQueryService
{
    public async Task<IEnumerable<ProfileReport>> Handle(GetProfileReportsQuery query)
        => await repository.ListAsync();
}
