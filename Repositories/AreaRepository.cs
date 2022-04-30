namespace Meals.Repositories;

public interface IAreaRepository
{
    Task<Area> AddArea(Area newArea);
    Task<Area> DeleteArea(string id);
    Task<List<Area>> GetAllAreas();
    Task<Area> GetArea(string id);
    Task<List<Area>> GetAreaById(string areaId);
    Task<List<Area>> GetAreaByName(string areaName);
    Task<Area> UpdateArea(Area area, string id);
}

public class AreaRepository : IAreaRepository
{
    private readonly IMongoContext _context;

    public AreaRepository(IMongoContext context)
    {
        _context = context;
    }


    public async Task<List<Area>> GetAllAreas()
    {
        return await _context.AreasCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Area> GetArea(string id)
    {
        return await _context.AreasCollection.Find<Area>(id).FirstOrDefaultAsync();
    }
    public async Task<List<Area>> GetAreaByName(string areaName) => await _context.AreasCollection.Find(a => a.AreaName == areaName).ToListAsync();
    public async Task<List<Area>> GetAreaById(string areaId) =>
await _context.AreasCollection.Find(a => a.Id == areaId).ToListAsync();

    public async Task<Area> AddArea(Area newArea)
    {
        await _context.AreasCollection.InsertOneAsync(newArea);
        return newArea;
    }

    public async Task<Area> DeleteArea(string id)
    {
        try
        {
            var filter = Builders<Area>.Filter.Eq("Id", id);
            var result = await _context.AreasCollection.DeleteOneAsync(filter);
            return await GetArea(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Area> UpdateArea(Area area, string id)
    {
        try
        {
            var filter = Builders<Area>.Filter.Eq("Id", id);
            var result = await _context.AreasCollection.ReplaceOneAsync(filter, area);
            return await GetArea(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }


}