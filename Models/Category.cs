namespace Meals.Models;

public class Category
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryThumb { get; set; }

}