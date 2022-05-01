namespace Meals.MealService;

public interface IMealService
{
    Task<Area> AddArea(Area newArea);
    Task<Category> AddCategory(Category newCategory);
    Task<Meal> AddMeal(Meal newMeal);
    Task<Area> DeleteArea(string id);
    Task<Category> DeleteCategory(string id);
    Task<Meal> DeleteMeal(string id);
    Task<List<Area>> GetAreas();
    Task<List<Category>> GetCategories();
    Task<List<Meal>> GetMeals();
    Task<List<Meal>> GetMealsByAreaName(string AreaName);
    Task<List<Area>> GetOneAreaById(string areaId);
    Task<List<Area>> GetOneAreaByName(string areaName);
    Task<List<Category>> GetOneCategoryById(string categoryId);
    Task<List<Category>> GetOneCategoryByName(string categoryName);
    Task<List<Meal>> GetOneMealByName(string MealName);
    Task SetupAreaData();
    Task SetupCategoryData();
    Task SetupMealData();
    Task<Area> UpdateArea(Area updateArea, string id);
    Task<Category> UpdateCategory(Category updateCategory, string id);
    Task<Meal> UpdateMeal(Meal updateMeal, string id);
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

    public async Task<List<Meal>> GetOneMealByName(string MealName) => await _mealRepository.GetMealByName(MealName);

    public async Task<List<Meal>> GetMealsByAreaName(string AreaName) => await _mealRepository.GetMealsByAreaName(AreaName);

    public async Task<Meal> DeleteMeal(string id) => await _mealRepository.DeleteMeal(id);
    public async Task<Meal> UpdateMeal(Meal updateMeal, string id) => await _mealRepository.UpdateMeal(updateMeal, id);

    public async Task SetupMealData()
    {
        if (!(await _mealRepository.GetAllMeals()).Any())
        {
            var areas = await _areaRepository.GetAllAreas();
            var categories = await _categoryRepository.GetAllCategorys();
            var meals = new List<Meal>(){
            new Meal()
            {
                MealName = "Pate Chinois",
                MealCategory = categories[0],
                MealArea = areas[2],
                MealInstructions = "In a large pot of salted water, cook the potatoes until they are very tender. Drain.\r\nWith a masher, coarsely crush the potatoes with at least 30 ml (2 tablespoons) of butter. With an electric mixer, purée with the milk. Season with salt and pepper. Set aside.\r\nWith the rack in the middle position, preheat the oven to 190 °C (375 °F).\r\nIn a large skillet, brown the onion in the remaining butter. Add the meat and cook until golden brown. Season with salt and pepper. Remove from the heat.\r\nLightly press the meat at the bottom of a 20-cm (8-inch) square baking dish. Cover with the corn and the mashed potatoes. Sprinkle with paprika and parsley.\r\nBake for about 30 minutes. Finish cooking under the broiler. Let cool for 10 minutes.",
                MealThumbImg = "https://cdn.pratico-pratiques.com/app/uploads/sites/4/2018/08/30184324/pate-chinois-revisite.jpeg",
                MealTags = "Main, Alcoholic",
                YoutubeLink = "https://www.youtube.com/watch?v=QRFqnLkEv3I",
                Ingredient1= "Potatoes",
                Ingredient2= "Butter",
                Ingredient3= "Milk",
                Ingredient4= "Minced Beef",
                Ingredient5= "Onion",
                Ingredient6= "Creamed Corn",
                Ingredient7= "Paprika",
                Ingredient8= "Parsley",
                Ingredient9= "Salt",
                Ingredient10= "Pepper",
                Measure1= "4 cups",
                Measure2= "60ml",
                Measure3= "½ cup",
                Measure4= "450g",
                Measure5= "1 finely chopped",
                Measure6 = "500ml",
                Measure7 = "to taste",
                Measure8 = "to taste",
                Measure9 = "Dash",
                Measure10 = "Dash",
                MealSource = "https://www.ricardocuisine.com/en/recipes/5541-pate-chinois-shepherd--s-pie-"
            },
            new Meal()
            {
                MealName = "Parkin Cake",
                MealCategory = categories[2],
                MealArea = areas[1],
                MealInstructions = "Heat oven to 160C/140C fan/gas 3. Grease a deep 22cm/9in square cake tin and line with baking parchment. Beat the egg and milk together with a fork.\r\n\r\nGently melt the syrup, treacle, sugar and butter together in a large pan until the sugar has dissolved. Remove from the heat. Mix together the oatmeal, flour and ginger and stir into the syrup mixture, followed by the egg and milk.\r\n\r\nPour the mixture into the tin and bake for 50 mins - 1 hr until the cake feels firm and a little crusty on top. Cool in the tin then wrap in more parchment and foil and keep for 3-5 days before eating if you can – it’ll become softer and stickier the longer you leave it, up to 2 weeks.\r\n",
                MealThumbImg = "https://images.immediate.co.uk/production/volatile/sites/30/2020/08/recipe-image-legacy-id-890458_10-8360227.jpg",
                MealTags = "Caramel",
                YoutubeLink = "https://www.youtube.com/watch?v=k1lG4vk2MQA",
                Ingredient1= "Butter",
                Ingredient2= "Egg",
                Ingredient3= "Milk",
                Ingredient4= "Golden Syrup",
                Ingredient5= "Black Treacle",
                Ingredient6= "Brown sugar",
                Ingredient7= "Oatmeal",
                Ingredient8= "Self-raising Flour",
                Ingredient9= "Ground Ginger",
                Ingredient10= "Pepper",
                Measure1= "200g",
                Measure2= "1 large",
                Measure3= "4 tbs",
                Measure4= "200g",
                Measure5= "85g",
                Measure6 = "85g",
                Measure7 = "100g",
                Measure8 = "250g",
                Measure9 = "1 tbs",
                MealSource = "https://www.bbcgoodfood.com/recipes/1940684/parkin"
            },
            new Meal()
            {
                MealName = "General Tso's Chicken",
                MealCategory = categories[1],
                MealArea = areas[2],
                MealInstructions = "DIRECTIONS:\r\nSTEP 1 - SAUCE\r\nIn a bowl, add 2 Cups of water, 2 Tablespoon soy sauce, 2 Tablespoon white vinegar, sherry cooking wine, 1/4 Teaspoon white pepper, minced ginger, minced garlic, hot pepper, ketchup, hoisin sauce, and sugar.\r\nMix together well and set aside.\r\nSTEP 2 - MARINATING THE CHICKEN\r\nIn a bowl, add the chicken, 1 pinch of salt, 1 pinch of white pepper, 2 egg whites, and 3 Tablespoon of corn starch\r\nSTEP 3 - DEEP FRY THE CHICKEN\r\nDeep fry the chicken at 350 degrees for 3-4 minutes or until it is golden brown and loosen up the chicken so that they don't stick together.\r\nSet the chicken aside.\r\nSTEP 4 - STIR FRY\r\nAdd the sauce to the wok and then the broccoli and wait until it is boiling.\r\nTo thicken the sauce, whisk together 2 Tablespoon of cornstarch and 4 Tablespoon of water in a bowl and slowly add to your stir-fry until it's the right thickness.\r\nNext add in the chicken and stir-fry for a minute and serve on a plate",
                MealThumbImg = "https://www.recipetineats.com/wp-content/uploads/2020/10/General-Tsao-Chicken_1-SQ.jpg",
                YoutubeLink = "https://www.youtube.com/watch?v=wWGwz0iBmvU",
                Ingredient1= "Chicken Breast",
                Ingredient2= "Plain Flour",
                Ingredient3= "Egg",
                Ingredient4= "Starch",
                Ingredient5= "Baking Powder",
                Ingredient6= "Salt",
                Ingredient7= "Onion Salt",
                Ingredient8= "Garlic Powder",
                Ingredient9= "Water",
                Ingredient10= "Chicken Stock",
                Ingredient11= "Duck Sauce",
                Ingredient12= "Soy Sauce",
                Ingredient13= "Honey",
                Ingredient14= "Rice Vinegar",
                Ingredient15= "Sesame Seed Oil",
                Ingredient16= "Gochujang",
                Ingredient17= "Starch",
                Ingredient18= "Garlic",
                Ingredient19= "Spring Onions",
                Ingredient20= "Ginger",
                Measure1 = "1 1/2 ",
                Measure2 = "3/4 cup ",
                Measure3 = "1",
                Measure4 = "2 tbs",
                Measure5 = "1 tbs",
                Measure6 = "1 tsp ",
                Measure7 = "1/2 tsp",
                Measure8 = "1/4 tsp",
                Measure9 = "3/4 cup ",
                Measure10 = "1/2 cup",
                Measure11 = "1/4 cup",
                Measure12 = "3 tbs",
                Measure13 = "2 tbs",
                Measure14 = "1 tbs",
                Measure15 = "2 tbs",
                Measure16 = "1/2 tbs",
                Measure17 = "2 tbs",
                Measure18 = "1  clove",
                Measure19 = "2 chopped",
                Measure20 = "1 tsp",
                MealSource = "https://www.skinnytaste.com/general-tsos-chicken/"
            },
            new Meal()
            {
                MealName = "Lasagna Sandwiches",
                MealCategory = categories[3],
                MealArea = areas[0],
                MealInstructions = "1. In a small bowl, combine the first four ingredients; spread on four slices of bread. Layer with bacon, tomato and cheese; top with remaining bread.\r\n\r\n2. In a large skillet or griddle, melt 2 tablespoons butter. Toast sandwiches until lightly browned on both sides and cheese is melted, adding butter if necessary.\r\n\r\nNutrition Facts\r\n1 sandwich: 445 calories, 24g fat (12g saturated fat), 66mg cholesterol, 1094mg sodium, 35g carbohydrate (3g sugars, 2g fiber), 21g protein.",
                MealThumbImg = "https://galbanicheese.com/wp-content/uploads/2019/09/Lasagna-Grilled-Cheese-e1481552597889-800x600.jpg",
                Ingredient1= "Sour Cream",
                Ingredient2= "Chopped Onion",
                Ingredient3= "Dried Oregano",
                Ingredient4= "Salt",
                Ingredient5= "Bread",
                Ingredient6= "Bacon",
                Ingredient7= "Tomato",
                Ingredient8= "Mozzarella",
                Ingredient9= "Butter",
                Measure1 = "1/4 cup",
                Measure2 = "2 tbs",
                Measure3 = "1/2 tbs",
                Measure4 = "1/4 tsp",
                Measure5 = "8 slices",
                Measure6 = "8 slices",
                Measure7 = "8 slices",
                Measure8 = "4 slices",
                Measure9 = "2 1/2 Tbs",
            },
        };

            foreach (var meal in meals)
                await _mealRepository.AddMeal(meal);
        }
    }

    //AREAS
    public async Task<Area> AddArea(Area newArea)
    {
        return await _areaRepository.AddArea(newArea);
    }

    public async Task<Area> DeleteArea(string id) => await _areaRepository.DeleteArea(id);

    public async Task<Area> UpdateArea(Area updateArea, string id) => await _areaRepository.UpdateArea(updateArea, id);

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
    public async Task<Category> UpdateCategory(Category updateCategory, string id) => await _categoryRepository.UpdateCategory(updateCategory, id);

    public async Task<Category> DeleteCategory(string id) => await _categoryRepository.DeleteCategory(id);

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