namespace Meals.Repositories;
public interface IMealRepository
{
    Task<Meal> AddMeal(Meal newMeal);
    Task DeleteMeal(string id);
    Task<List<Meal>> GetAllMeals();
    Task<Meal> GetMeal(string id);

    Task<Meal> GetMealByName(string id);
}

public class MealRepository : IMealRepository
{
    private readonly IMongoContext _context;
    public MealRepository(IMongoContext context)
    {
        _context = context;
    }

    //gevaarlijk wordt per keer opgevragen betaald. Kijken voor eventueel betere optie.
    public async Task<List<Meal>> GetAllMeals()
    {
        return await _context.MealsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Meal> GetMeal(string id) => await _context.MealsCollection.Find<Meal>(id).FirstOrDefaultAsync();

    public async Task<Meal> GetMealByName(string MealName) => await _context.MealsCollection.Find<Meal>(MealName).FirstOrDefaultAsync();

    public async Task<List<Meal>> GetMealsByAreaId(string areaId) =>
    await _context.MealsCollection.Find(a => a.MealArea.Id == areaId).ToListAsync();

    public async Task<Meal> AddMeal(Meal newMeal)
    {
        await _context.MealsCollection.InsertOneAsync(newMeal);
        return newMeal;
    }

    public async Task DeleteMeal(string id)
    {
        try
        {
            var filter = Builders<Meal>.Filter.Eq("Id", id);
            var result = await _context.MealsCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Meal> UpdateMeal(Meal meal)
    {
        try
        {
            var filter = Builders<Meal>.Filter.Eq("Id", meal.Id);
            var update = Builders<Meal>.Update.Set("Name", meal.MealName).Set("Instructions", meal.MealInstructions);
            var result = await _context.MealsCollection.UpdateOneAsync(filter, update);
            return await GetMeal(meal.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }


}