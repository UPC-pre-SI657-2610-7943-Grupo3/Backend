namespace HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;

/// <summary>
///     Service category aggregate: cleaning, cooking, gardening, child-care, etc.
/// </summary>
public class ServiceCategory
{
    public int Id { get; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public ServiceCategory() { }

    public ServiceCategory(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public ServiceCategory UpdateDescription(string description)
    {
        Description = description;
        return this;
    }
}
