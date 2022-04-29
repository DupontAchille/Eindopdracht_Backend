namespace Meals.GraphQL.Queries;

public class Queries
{
    public async Task<List<Area>> GetAreas([Service]
    IMealService mealService) => await mealService.GetAreas();
    public async Task<List<Category>> GetCategories([Service]
    IMealService mealService) => await mealService.GetCategories();
    public async Task<List<Meal>> GetMeals([Service]
    IMealService mealService) => await mealService.GetMeals();
}