namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Commands;

public record CreateProfileReportCommand(
    int ReportedUserId,
    int ReporterUserId,
    string Reason,
    string Description);
