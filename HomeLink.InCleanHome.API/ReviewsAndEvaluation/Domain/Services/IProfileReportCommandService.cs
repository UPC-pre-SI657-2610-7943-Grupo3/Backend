using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Commands;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Services;

public interface IProfileReportCommandService
{
    Task<ProfileReport?> Handle(CreateProfileReportCommand command);
}
