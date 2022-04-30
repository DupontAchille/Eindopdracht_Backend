namespace Meals.Repositories;

public interface ICategoryRepository
{
    Task<Category> AddCategory(Category newCategory);
    Task<Category> DeleteCategory(string id);
    Task<List<Category>> GetAllCategorys();
    Task<Category> GetCategory(string id);
    Task<List<Category>> GetCategoryById(string categoryId);
    Task<List<Category>> GetCategoryByName(string categoryName);
    Task<Category> UpdateCategory(Category category, string id);
}

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoContext _context;

    public CategoryRepository(IMongoContext context)
    {
        _context = context;
    }

    //gevaarlijk wordt per keer opgevragen betaald. Kijken voor eventueel betere optie.
    public async Task<List<Category>> GetAllCategorys()
    {
        return await _context.CategoriesCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Category> GetCategory(string id) => await _context.CategoriesCollection.Find<Category>(id).FirstOrDefaultAsync();

    public async Task<List<Category>> GetCategoryByName(string categoryName) => await _context.CategoriesCollection.Find(c => c.CategoryName == categoryName).ToListAsync();

    public async Task<List<Category>> GetCategoryById(string categoryId) => await _context.CategoriesCollection.Find(c => c.Id == categoryId).ToListAsync();

    public async Task<Category> AddCategory(Category newCategory)
    {
        await _context.CategoriesCollection.InsertOneAsync(newCategory);
        return newCategory;
    }
    public async Task<Category> DeleteCategory(string id)
    {
        try
        {
            var filter = Builders<Category>.Filter.Eq("Id", id);
            var result = await _context.CategoriesCollection.DeleteOneAsync(filter);
            return await GetCategory(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Category> UpdateCategory(Category category, string id)
    {
        try
        {
            var filter = Builders<Category>.Filter.Eq("Id", id);
            var result = await _context.CategoriesCollection.ReplaceOneAsync(filter, category);
            return await GetCategory(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }



}