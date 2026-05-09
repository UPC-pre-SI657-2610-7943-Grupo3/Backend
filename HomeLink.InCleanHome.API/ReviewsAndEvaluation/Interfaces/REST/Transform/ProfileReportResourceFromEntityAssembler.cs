using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Resources;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Transform;

public static class ProfileReportResourceFromEntityAssembler
{
    public static ProfileReportResource ToResourceFromEntity(ProfileReport entity)
        => new(entity.Id, entity.ReportedUserId, entity.ReporterUserId, entity.Reason, entity.Description, entity.Status);
}
