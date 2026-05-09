namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Commands;

public record CreateWorkerServiceCommand(
    int WorkerProfileId,
    int ServiceCategoryId,
    string Title,
    string Description,
    decimal Price);
