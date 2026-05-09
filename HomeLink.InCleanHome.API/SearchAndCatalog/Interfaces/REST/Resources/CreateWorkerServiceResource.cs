namespace HomeLink.InCleanHome.API.SearchAndCatalog.Interfaces.REST.Resources;

public record CreateWorkerServiceResource(
    int WorkerProfileId,
    int ServiceCategoryId,
    string Title,
    string Description,
    decimal Price);
