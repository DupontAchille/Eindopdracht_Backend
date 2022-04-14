namespace Meals.Repositories;

public class AreaRepository
{
    private readonly IMongoContext _context;

    public AreaRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Area> AddArea(Area newArea)
    {
        await _context.AreasCollection.InsertOneAsync(newArea);
        return newArea;
    }

    public async Task DeleteArea(string id)
    {
        try
        {
            var filter = Builders<Area>.Filter.Eq("Id", id);
            var result = await _context.AreasCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Area> UpdateArea(Area area)
    {
        try
        {
            var filter = Builders<Area>.Filter.Eq("Id", area.Id);
            var update = Builders<Area>.Update.Set("Name", area.AreaName);
            var result = await _context.AreasCollection.UpdateOneAsync(filter, update);
            return await GetArea(area.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<Area>> GetAllAreas()
    {
        return await _context.AreasCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Area> GetArea(string id) => await _context.AreasCollection.Find<Area>(id).FirstOrDefaultAsync();


}