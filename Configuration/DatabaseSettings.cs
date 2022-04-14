namespace Meals.Configuration;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? MealsCollection { get; set; }
    public string? AreasCollection { get; set; }
    public string? CategoriesCollection { get; set; }
}