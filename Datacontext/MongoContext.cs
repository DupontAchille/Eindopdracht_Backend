namespace Meals.Context;

public interface IMongoContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Meal> MealsCollection { get; }
    IMongoCollection<Area> AreasCollection { get; }
    IMongoCollection<Category> CategoriesCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Meal> MealsCollection
    {
        get
        {
            return _database.GetCollection<Meal>(_settings.MealsCollection);
        }
    }

    public IMongoCollection<Area> AreasCollection
    {
        get
        {
            return _database.GetCollection<Area>(_settings.AreasCollection);
        }
    }
    public IMongoCollection<Category> CategoriesCollection
    {
        get
        {
            return _database.GetCollection<Category>(_settings.CategoriesCollection);
        }
    }
}