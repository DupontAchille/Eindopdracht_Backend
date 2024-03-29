namespace Meals.Repositories;

public interface IMealRepository
{
    Task<Meal> AddMeal(Meal newMeal);
    Task<Meal> DeleteMeal(string id);
    Task<List<Meal>> GetAllMeals();
    Task<Meal> GetMeal(string id);
    Task<List<Meal>> GetMealByName(string MealName);
    Task<List<Meal>> GetMealsByAreaName(string areaName);
    Task<Meal> UpdateMeal(Meal meal, string id);
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

    public async Task<List<Meal>> GetMealByName(string MealName) => await _context.MealsCollection.Find(m => m.MealName == MealName).ToListAsync();

    public async Task<List<Meal>> GetMealsByAreaName(string areaName) =>
    await _context.MealsCollection.Find(a => a.MealArea.AreaName == areaName).ToListAsync();

    public async Task<Meal> AddMeal(Meal newMeal)
    {
        await _context.MealsCollection.InsertOneAsync(newMeal);
        return newMeal;
    }

    public async Task<Meal> DeleteMeal(string id)
    {
        try
        {
            var filter = Builders<Meal>.Filter.Eq("Id", id);
            var result = await _context.MealsCollection.DeleteOneAsync(filter);
            return await GetMeal(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Meal> UpdateMeal(Meal meal, string id)
    {
        try
        {
            var filter = Builders<Meal>.Filter.Eq("Id", id);
            var result = await _context.MealsCollection.ReplaceOneAsync(filter, meal);
            return await GetMeal(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }


}