using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Commands;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Repositories;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Services;
using HomeLink.InCleanHome.API.Shared.Domain.Repositories;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Application.Internal.CommandServices;

public class ProfileReportCommandService(
    IProfileReportRepository repository,
    IUnitOfWork unitOfWork) : IProfileReportCommandService
{
    public async Task<ProfileReport?> Handle(CreateProfileReportCommand command)
    {
        var report = new ProfileReport(command);
        await repository.AddAsync(report);
        await unitOfWork.CompleteAsync();
        return report;
    }
}
