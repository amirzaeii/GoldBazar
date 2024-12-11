using AutoMapper;
using Catalog.Api.Infrastructure;
using GoldBazar.Shared.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Apis;

public static class GoldSmithApi
{
    public static IEndpointRouteBuilder MapGoldSmithApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/goldsmith").HasApiVersion(1.0);

        // CRUD Endpoints
        api.MapGet("/", GetAllGoldSmiths);
        api.MapGet("/{id:int}", GetGoldsmithById);
        api.MapPost("/", CreateGoldsmithAsync);
        api.MapPut("/{id:int}", UpdateGoldsmith);
        api.MapDelete("/{id:int}", DeleteGoldsmith);

        // Additional Endpoints
        api.MapGet("/city/{city}", GetGoldsmithsByCity);
        api.MapGet("/search", SearchGoldsmithsByCities);

        return app;
    }

    // Basic CRUD
    public static async Task<Ok<List<GoldSmithViewModel>>> GetAllGoldSmiths(CatalogContext context, IMapper mapper)
    {
        var goldsmiths = await context.GoldSmiths.ToListAsync();
        var goldsmithViewModels = mapper.Map<List<GoldSmithViewModel>>(goldsmiths);
        return TypedResults.Ok(goldsmithViewModels);
    }

    public static async Task<Results<Ok<GoldSmithViewModel>, NotFound>> GetGoldsmithById(CatalogContext context, IMapper mapper, int id)
    {
        var goldsmith = await context.GoldSmiths.FindAsync(id);
        if (goldsmith == null) return TypedResults.NotFound();

        var goldsmithViewModel = mapper.Map<GoldSmithViewModel>(goldsmith);
        return TypedResults.Ok(goldsmithViewModel);
    }

    public static async Task<Results<Created<int>, BadRequest>> CreateGoldsmithAsync(CatalogContext context, GoldSmithViewModel goldsmithViewModel, IMapper mapper)
    {
        try
        {
            // Ensure correct mapping
            var goldsmith = mapper.Map<GoldSmith>(goldsmithViewModel);

            // Validate the entity
            if (string.IsNullOrWhiteSpace(goldsmith.Name) || string.IsNullOrWhiteSpace(goldsmith.City))
            {
                return TypedResults.BadRequest();
            }

            // Add the entity and save changes
            context.GoldSmiths.Add(goldsmith);
            await context.SaveChangesAsync();

            // Return Created result
            return TypedResults.Created<int>($"/api/goldsmith/{goldsmith.Id}", goldsmith.Id);
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"An error occurred while updating the database: {ex.Message}");
            return TypedResults.BadRequest();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return TypedResults.BadRequest();
        }
    }


    public static async Task<Results<NoContent, NotFound>> UpdateGoldsmith(CatalogContext context, IMapper mapper, int id, GoldSmithViewModel updatedGoldsmith)
    {
        var goldsmith = await context.GoldSmiths.FindAsync(id);
        if (goldsmith == null) return TypedResults.NotFound();

        mapper.Map(updatedGoldsmith, goldsmith);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> DeleteGoldsmith(CatalogContext context, int id)
    {
        var goldsmith = await context.GoldSmiths.FindAsync(id);
        if (goldsmith == null) return TypedResults.NotFound();

        context.GoldSmiths.Remove(goldsmith);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    // Additional Endpoints
    public static async Task<Ok<List<GoldSmithViewModel>>> GetGoldsmithsByCity(CatalogContext context, IMapper mapper, string city)
    {
        var goldsmiths = await context.GoldSmiths
            .Where(g => g.City.Equals(city, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
        var goldsmithViewModels = mapper.Map<List<GoldSmithViewModel>>(goldsmiths);
        return TypedResults.Ok(goldsmithViewModels);
    }

    public static async Task<Ok<List<GoldSmithViewModel>>> SearchGoldsmithsByCities(CatalogContext context, IMapper mapper, string[] cities)
    {
        var goldsmiths = await context.GoldSmiths
            .Where(g => cities.Contains(g.City))
            .ToListAsync();
        var goldsmithViewModels = mapper.Map<List<GoldSmithViewModel>>(goldsmiths);
        return TypedResults.Ok(goldsmithViewModels);
    }
}
