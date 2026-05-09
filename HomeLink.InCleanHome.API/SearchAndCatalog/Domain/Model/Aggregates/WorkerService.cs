using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Commands;

namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;

/// <summary>
///     A specific service offered by a worker (e.g., "Limpieza profunda Lima Norte").
/// </summary>
public class WorkerService
{
    public int Id { get; }
    public int WorkerProfileId { get; private set; }
    public int ServiceCategoryId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; } = true;

    public WorkerService() { }

    public WorkerService(CreateWorkerServiceCommand command)
    {
        WorkerProfileId = command.WorkerProfileId;
        ServiceCategoryId = command.ServiceCategoryId;
        Title = command.Title;
        Description = command.Description;
        Price = command.Price;
        IsActive = true;
    }

    public WorkerService Deactivate() { IsActive = false; return this; }
    public WorkerService Activate() { IsActive = true; return this; }
}
