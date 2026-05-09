using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Commands;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Transform;

public static class CreateProfileReportCommandFromResourceAssembler
{
    public static CreateProfileReportCommand ToCommandFromResource(CreateProfileReportResource resource)
        => new(resource.ReportedUserId, resource.ReporterUserId, resource.Reason, resource.Description);
}
