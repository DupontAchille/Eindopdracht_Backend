namespace Meals.Models;

public class Meal
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? MealName { get; set; }
    public Category? MealCategory { get; set; }

    public Area? MealArea { get; set; }

    public string? MealInstructions { get; set; }

    public string? MealThumbImg { get; set; }

    public string? MealTags { get; set; }

    public string? YoutubeLink { get; set; }

    public string? Ingredient1 { get; set; }
    public string? Ingredient2 { get; set; }
    public string? Ingredient3 { get; set; }
    public string? Ingredient4 { get; set; }
    public string? Ingredient5 { get; set; }
    public string? Ingredient6 { get; set; }
    public string? Ingredient7 { get; set; }
    public string? Ingredient8 { get; set; }
    public string? Ingredient9 { get; set; }
    public string? Ingredient10 { get; set; }
    public string? Ingredient11 { get; set; }
    public string? Ingredient12 { get; set; }
    public string? Ingredient13 { get; set; }
    public string? Ingredient14 { get; set; }
    public string? Ingredient15 { get; set; }
    public string? Measure1 { get; set; }
    public string? Measure2 { get; set; }
    public string? Measure3 { get; set; }
    public string? Measure4 { get; set; }
    public string? Measure5 { get; set; }
    public string? Measure6 { get; set; }
    public string? Measure7 { get; set; }
    public string? Measure8 { get; set; }
    public string? Measure9 { get; set; }
    public string? Measure10 { get; set; }
    public string? Measure11 { get; set; }
    public string? Measure12 { get; set; }
    public string? Measure13 { get; set; }
    public string? Measure14 { get; set; }
    public string? Measure15 { get; set; }
    public DateTime CreatedOn { get; set; }
}