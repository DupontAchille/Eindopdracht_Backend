namespace Meals.Models;

public class Area
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? AreaName { get; set; }

    public string? AreaContinent { get; set; }
}