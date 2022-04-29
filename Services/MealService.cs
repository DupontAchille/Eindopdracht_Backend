namespace Meals.MealService;

public interface IMealService
{
    Task<Area> AddArea(Area newArea);
    Task<Category> AddCategory(Category newCategory);
    Task<Meal> AddMeal(Meal newMeal);
    Task<Area> DeleteArea(string id);
    Task<List<Area>> GetAreas();
    Task<List<Category>> GetCategories();
    Task<List<Meal>> GetMeals();
    Task<List<Area>> GetOneAreaById(string areaId);
    Task<List<Area>> GetOneAreaByName(string areaName);
    Task<List<Category>> GetOneCategoryById(string categoryId);
    Task<List<Category>> GetOneCategoryByName(string categoryName);
    Task SetupAreaData();
    Task SetupCategoryData();
    Task<Area> UpdateArea(string id, Area area);
}

public class MealService : IMealService
{
    private readonly IMealRepository _mealRepository;

    private readonly IAreaRepository _areaRepository;

    private readonly ICategoryRepository _categoryRepository;


    public MealService(IMealRepository mealRepository, IAreaRepository areaRepository, ICategoryRepository categoryRepository)
    {
        _mealRepository = mealRepository;
        _areaRepository = areaRepository;
        _categoryRepository = categoryRepository;
    }


    //MEALS
    public async Task<Meal> AddMeal(Meal newMeal)
    {
        return await _mealRepository.AddMeal(newMeal);
    }

    public async Task<List<Meal>> GetMeals() => await _mealRepository.GetAllMeals();

    //AREAS
    public async Task<Area> AddArea(Area newArea)
    {
        return await _areaRepository.AddArea(newArea);
    }

    public async Task<Area> DeleteArea(string id) => await _areaRepository.DeleteArea(id);

    public async Task<Area> UpdateArea(string id, Area area) => await _areaRepository.UpdateArea(id, area);

    public async Task<List<Area>> GetAreas() => await _areaRepository.GetAllAreas();

    public async Task<List<Area>> GetOneAreaById(string areaId)
    {
        return await _areaRepository.GetAreaById(areaId);
    }

    public async Task<List<Area>> GetOneAreaByName(string areaName) => await _areaRepository.GetAreaByName(areaName);
    public async Task SetupAreaData()
    {
        if (!(await _areaRepository.GetAllAreas()).Any())
        {
            var areas = new List<Area>(){
                new Area(){
                    AreaName = "American", AreaContinent = "America"
                },
                new Area(){
                    AreaName = "British", AreaContinent = "Europe"
                },
                new Area(){
                    AreaName = "Chinese", AreaContinent = "Asia"
                },
                new Area(){
                    AreaName = "Indian", AreaContinent = "Asia"
                },
                new Area(){
                    AreaName = "Croatian", AreaContinent = "Europe"
                },
                new Area(){
                    AreaName = "Egyptian", AreaContinent = "Africa"
                },
            };
            foreach (var area in areas)
                await _areaRepository.AddArea(area);
        }
    }


    //CATEGORIES
    public async Task<Category> AddCategory(Category newCategory)
    {
        return await _categoryRepository.AddCategory(newCategory);
    }

    public async Task<List<Category>> GetCategories() => await _categoryRepository.GetAllCategorys();

    public async Task<List<Category>> GetOneCategoryById(string categoryId)
    {
        return await _categoryRepository.GetCategoryById(categoryId);
    }

    public async Task<List<Category>> GetOneCategoryByName(string categoryName)
    {
        return await _categoryRepository.GetCategoryByName(categoryName);
    }

    public async Task SetupCategoryData()
    {
        if (!(await _categoryRepository.GetAllCategorys()).Any())
        {

            var categories = new List<Category>(){
            new Category()
            {
            CategoryName = "Beef" , CategoryThumb = "https://upload.wikimedia.org/wikipedia/commons/a/a3/Rostas_%28ready_and_served%29.JPG"
            },
            new Category()
            {
            CategoryName = "Chicken" , CategoryThumb = "https://images.unsplash.com/photo-1606728035253-49e8a23146de?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8M3x8Y2hpY2tlbiUyMGZvb2R8ZW58MHx8MHx8&w=1000&q=80"
            },
            new Category()
            {
            CategoryName = "Lamb" , CategoryThumb = "https://media.istockphoto.com/photos/rack-of-lamb-with-rosemary-picture-id105489252?k=20&m=105489252&s=612x612&w=0&h=mJk5cHlPqf3-h1Idwgrd1-EjHA3F8_KLTjo-Enf5HKs="
            },
            new Category()
            {
            CategoryName = "Pasta" , CategoryThumb = "https://images.unsplash.com/photo-1551183053-bf91a1d81141?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTR8fHBhc3RhfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60"
            },
            new Category()
            {
            CategoryName = "Pork" , CategoryThumb = "https://images.unsplash.com/photo-1547050605-2f268cd5daf0?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"
            },
        };

            foreach (var category in categories)
                await _categoryRepository.AddCategory(category);
        }
    }
}