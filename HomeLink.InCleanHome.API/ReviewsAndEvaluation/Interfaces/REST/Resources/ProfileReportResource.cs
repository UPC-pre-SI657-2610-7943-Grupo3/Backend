namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Interfaces.REST.Resources;

public record ProfileReportResource(
    int Id,
    int ReportedUserId,
    int ReporterUserId,
    string Reason,
    string Description,
    string Status);
