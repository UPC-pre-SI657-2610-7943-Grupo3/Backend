namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;

public record WorkerServiceResource(
    int Id,
    int WorkerProfileId,
    int ServiceCategoryId,
    string Title,
    string Description,
    decimal Price,
    bool IsActive);
