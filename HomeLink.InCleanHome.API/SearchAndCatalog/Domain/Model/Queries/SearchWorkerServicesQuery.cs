namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Queries;

/// <summary>
///     Search filter for finding services. All fields are optional.
/// </summary>
public record SearchWorkerServicesQuery(
    int? ServiceCategoryId,
    string? District,
    decimal? MaxPrice,
    int? DayOfWeek);
