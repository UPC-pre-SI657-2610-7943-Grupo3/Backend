using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Commands;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.ValueObjects;

namespace HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;

/// <summary>
///     Suspicious-profile report aggregate root (US-17 / US-28).
/// </summary>
public partial class ProfileReport
{
    public int Id { get; }
    public int ReportedUserId { get; private set; }
    public int ReporterUserId { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Status { get; private set; } = ReportStatus.Pending;

    public ProfileReport() { }

    public ProfileReport(CreateProfileReportCommand command)
    {
        ReportedUserId = command.ReportedUserId;
        ReporterUserId = command.ReporterUserId;
        Reason = command.Reason;
        Description = command.Description;
        Status = ReportStatus.Pending;
    }

    public ProfileReport MarkReviewed() { Status = ReportStatus.Reviewed; return this; }
    public ProfileReport Dismiss() { Status = ReportStatus.Dismissed; return this; }
}
