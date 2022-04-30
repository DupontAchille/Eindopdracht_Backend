namespace Meals.GraphQL.Mutations;

public class Mutation
{
    public async Task<AddAreaPayload> AddArea([Service] IMealService mealService, AddAreaInput input)
    {
        var newArea = new Area()
        {
            AreaName = input.Name,
            AreaContinent = input.AreaContinent

        };
        var created = await mealService.AddArea(newArea);
        return new AddAreaPayload(created);
    }
}