namespace Meals.Repositories;

public interface ICategoryRepository
{
    Task<Category> AddCategory(Category newCategory);
    Task DeleteCategory(string id);
    Task<List<Category>> GetAllCategorys();
    Task<Category> GetCategory(string id);
    Task<Category> UpdateCategory(Category category);
}

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoContext _context;

    public CategoryRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Category> AddCategory(Category newCategory)
    {
        await _context.CategoriesCollection.InsertOneAsync(newCategory);
        return newCategory;
    }
    public async Task DeleteCategory(string id)
    {
        try
        {
            var filter = Builders<Category>.Filter.Eq("Id", id);
            var result = await _context.CategoriesCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Category> UpdateCategory(Category category)
    {
        try
        {
            var filter = Builders<Category>.Filter.Eq("Id", category.Id);
            var update = Builders<Category>.Update.Set("Name", category.CategoryName);
            var result = await _context.CategoriesCollection.UpdateOneAsync(filter, update);
            return await GetCategory(category.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //gevaarlijk wordt per keer opgevragen betaald. Kijken voor eventueel betere optie.
    public async Task<List<Category>> GetAllCategorys()
    {
        return await _context.CategoriesCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Category> GetCategory(string id) => await _context.CategoriesCollection.Find<Category>(id).FirstOrDefaultAsync();

}