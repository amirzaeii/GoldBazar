using Catalog.Api.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GoldSmithApi;

public static class GoldSmithApi
{
    public static IEndpointRouteBuilder MapGoldSmithApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/goldsmith").HasApiVersion(1.0);

        // CRUD Endpoints
        api.MapGet("/", GetAllGoldSmiths);
        api.MapGet("/{id:int}", GetGoldsmithById);
        api.MapPost("/", CreateGoldsmith);
        api.MapPut("/{id:int}", UpdateGoldsmith);
        api.MapDelete("/{id:int}", DeleteGoldsmith);

        // Additional Endpoints
        api.MapGet("/city/{city}", GetGoldsmithsByCity);
        api.MapGet("/search", SearchGoldsmithsByCities);


        // Reviews and Ratings Endpoints

        return app;
    }

    // Basic CRUD
    public static async Task<Ok<List<GoldSmithViewModel>>> GetAllGoldSmiths(CatalogContext context)
    {
        var goldsmiths = await context.GoldSmiths.ToListAsync();
        return TypedResults.Ok(goldsmiths);
    }

    public static async Task<Results<Ok<GoldSmithViewModel>, NotFound>> GetGoldsmithById(CatalogContext context, int id)
    {
        var goldsmith = await context.GoldSmiths.FindAsync(id);
        return goldsmith == null ? TypedResults.NotFound() : TypedResults.Ok(goldsmith);
    }

    public static async Task<Created> CreateGoldsmith(CatalogContext context, GoldSmithViewModel goldsmith)
    {
        context.GoldSmiths.Add(goldsmith);
        await context.SaveChangesAsync();
        return TypedResults.Created($"/api/goldsmith/{goldsmith.Id}");
    }

    public static async Task<Results<NoContent, NotFound>> UpdateGoldsmith(CatalogContext context, int id, GoldSmithViewModel updatedGoldsmith)
    {
        var goldsmith = await context.GoldSmiths.FindAsync(id);
        if (goldsmith == null)
        {
            return TypedResults.NotFound();
        }

        context.Entry(goldsmith).CurrentValues.SetValues(updatedGoldsmith);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> DeleteGoldsmith(CatalogContext context, int id)
    {
        var goldsmith = await context.GoldSmiths.FindAsync(id);
        if (goldsmith == null)
        {
            return TypedResults.NotFound();
        }

        context.GoldSmiths.Remove(goldsmith);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    // Get Goldsmiths by City
    public static async Task<Ok<List<GoldSmithViewModel>>> GetGoldsmithsByCity(CatalogContext context, string city)
    {
        var goldsmiths = await context.GoldSmiths
            .Where(g => g.City.Equals(city, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
        return TypedResults.Ok(goldsmiths);
    }

    // Search Goldsmiths by Cities
    public static async Task<Ok<List<GoldSmithViewModel>>> SearchGoldsmithsByCities(CatalogContext context, string[] cities)
    {
        var goldsmiths = await context.GoldSmiths
            .Where(g => cities.Contains(g.City))
            .ToListAsync();
        return TypedResults.Ok(goldsmiths);
    }
}
