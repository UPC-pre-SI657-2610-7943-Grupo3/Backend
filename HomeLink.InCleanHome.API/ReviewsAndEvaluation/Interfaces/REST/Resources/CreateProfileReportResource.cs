namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Resources;

public record CreateProfileReportResource(
    int ReportedUserId,
    int ReporterUserId,
    string Reason,
    string Description);
