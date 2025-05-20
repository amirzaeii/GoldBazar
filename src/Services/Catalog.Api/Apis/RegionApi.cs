using GoldBazar.Shared.DTOs;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Apis;

public static class RegionApi
{
    public static IEndpointRouteBuilder MapRegionApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/catalog").HasApiVersion(1.0);

        // Governorate Endpoints
        api.MapGet("/governorates", GetAllGovernorates)
            .WithName("GovernorateList")
            .WithSummary("List of governorates")
            .WithDescription("Get all governorates.")
            .WithTags("Governorate");

        api.MapGet("/governorates/{id:int}", GetGovernorateById)
            .WithName("GetGovernorateById")
            .WithSummary("Get governorate by ID")
            .WithDescription("Retrieve a specific governorate by its ID.")
            .WithTags("Governorate");

        api.MapPost("/governorates", AddGovernorate)
            .WithName("AddGovernorate")
            .WithSummary("Add a new governorate")
            .WithDescription("Create a new governorate entry.")
            .WithTags("Governorate");

        api.MapPut("/governorates/{id:int}", UpdateGovernorate)
            .WithName("UpdateGovernorate")
            .WithSummary("Update governorate details")
            .WithDescription("Modify an existing governorate.")
            .WithTags("Governorate");

        api.MapDelete("/governorates/{id:int}", DeleteGovernorate)
            .WithName("DeleteGovernorate")
            .WithSummary("Delete a governorate")
            .WithDescription("Remove a governorate by its ID.")
            .WithTags("Governorate");

        // City Endpoints
        api.MapGet("/cities", GetAllCities)
            .WithName("CityList")
            .WithSummary("List of cities")
            .WithDescription("Get all cities, including their governorate names.")
            .WithTags("City");

        api.MapGet("/cities/{id:int}", GetCityById)
            .WithName("GetCityById")
            .WithSummary("Get city by ID")
            .WithDescription("Retrieve a specific city by its ID.")
            .WithTags("City");

        api.MapGet("/cities/governorate/{governorateId:int}", GetCitiesByGovernorate)
            .WithName("GetCitiesByGovernorate")
            .WithSummary("Get cities by governorate ID")
            .WithDescription("List all cities within a given governorate.")
            .WithTags("City");

        api.MapPost("/cities", AddCity)
            .WithName("AddCity")
            .WithSummary("Add a new city")
            .WithDescription("Create a new city under a specified governorate.")
            .WithTags("City");

        api.MapPut("/cities/{id:int}", UpdateCity)
            .WithName("UpdateCity")
            .WithSummary("Update city details")
            .WithDescription("Modify an existing city's information.")
            .WithTags("City");

        api.MapDelete("/cities/{id:int}", DeleteCity)
            .WithName("DeleteCity")
            .WithSummary("Delete a city")
            .WithDescription("Remove a city by its ID.")
            .WithTags("City");

        return app;
    }

    // Governorates
    // GET /api/catalog/governorates
    public static async Task<Results<Ok<GovernateDTO[]>, NotFound>> GetAllGovernorates(
        [AsParameters] CatalogServices services)
    {
        var list = await services.Context.Governorates
            .AsNoTracking()
            .Select(g => new GovernateDTO(g.Id, g.Name))
            .ToArrayAsync();

        return list.Length > 0
            ? TypedResults.Ok(list)
            : TypedResults.NotFound();
    }

    // GET /api/catalog/governorates/{id}
    public static async Task<Results<Ok<GovernateDTO>, NotFound>> GetGovernorateById(
        int id,
        [AsParameters] CatalogServices services)
    {
        var dto = await services.Context.Governorates
            .AsNoTracking()
            .Where(g => g.Id == id)
            .Select(g => new GovernateDTO(g.Id, g.Name))
            .FirstOrDefaultAsync();

        return dto is not null
            ? TypedResults.Ok(dto)
            : TypedResults.NotFound();
    }

    // POST /api/catalog/governorates
    public static async Task<Results<Created<GovernateDTO>, BadRequest<string>>> AddGovernorate(
        [FromBody] GovernateDTO dto,
        [AsParameters] CatalogServices services)
    {
        // duplicate‐name guard
        if (await services.Context.Governorates
                .AnyAsync(g => g.Name == dto.Name))
        {
            return TypedResults.BadRequest(
                $"A governorate named '{dto.Name}' already exists.");
        }

        var entity = new Governorate { Name = dto.Name };
        services.Context.Governorates.Add(entity);
        await services.Context.SaveChangesAsync();

        var result = new GovernateDTO(entity.Id, entity.Name);
        return TypedResults.Created(
            $"/api/catalog/governorates/{entity.Id}",
            result);
    }

    // PUT /api/catalog/governorates/{id}
    public static async Task<Results<Ok<GovernateDTO>, NotFound, BadRequest<string>>> UpdateGovernorate(
        int id,
        [FromBody] GovernateDTO dto,
        [AsParameters] CatalogServices services)
    {
        var entity = await services.Context.Governorates.FindAsync(id);
        if (entity is null)
            return TypedResults.NotFound();

        // name‐collision guard
        if (await services.Context.Governorates
                .AnyAsync(g => g.Name == dto.Name && g.Id != id))
        {
            return TypedResults.BadRequest(
                $"A governorate named '{dto.Name}' already exists.");
        }

        entity.Name = dto.Name;
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok(
            new GovernateDTO(entity.Id, entity.Name));
    }

    // DELETE /api/catalog/governorates/{id}
    public static async Task<Results<Ok<string>, NotFound>> DeleteGovernorate(
        int id,
        [AsParameters] CatalogServices services)
    {
        var entity = await services.Context.Governorates.FindAsync(id);
        if (entity is null)
            return TypedResults.NotFound();

        services.Context.Governorates.Remove(entity);
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok($"Governorate with ID {id} deleted.");
    }

    // Cities
    public static async Task<Results<Ok<CityDTO[]>, NotFound>> GetAllCities([AsParameters] CatalogServices services)
    {
        var list = await services.Context.Cities
            .Include(c => c.Governorate)
            .AsNoTracking()
            .Select(c => new CityDTO(c.Id, c.Name, c.GovernorateId, c.Governorate.Name))
            .ToArrayAsync();

        return list.Length > 0
            ? TypedResults.Ok(list)
            : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<CityDTO>, NotFound>> GetCityById(
        int id,
        [AsParameters] CatalogServices services)
    {
        var dto = await services.Context.Cities
            .Include(c => c.Governorate)
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new CityDTO(c.Id, c.Name, c.GovernorateId, c.Governorate.Name))
            .FirstOrDefaultAsync();

        return dto is not null
            ? TypedResults.Ok(dto)
            : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<CityDTO[]>, NotFound>> GetCitiesByGovernorate(
        int governorateId,
        [AsParameters] CatalogServices services)
    {
        var list = await services.Context.Cities
            .Where(c => c.GovernorateId == governorateId)
            .Include(c => c.Governorate)
            .AsNoTracking()
            .Select(c => new CityDTO(c.Id, c.Name, c.GovernorateId, c.Governorate.Name))
            .ToArrayAsync();

        return list.Length > 0
            ? TypedResults.Ok(list)
            : TypedResults.NotFound();
    }

    public static async Task<Results<Created<CityDTO>, BadRequest<string>>> AddCity(
       [FromBody] CreateCityDTO dto,
       [AsParameters] CatalogServices services)
    {

        if (!await services.Context.Governorates
                       .AnyAsync(g => g.Id == dto.GovernorateId))
        {
            return TypedResults.BadRequest(
                $"Governorate ID {dto.GovernorateId} not found.");
        }

        if (await services.Context.Cities
                       .AnyAsync(c => c.Name == dto.Name
                                   && c.GovernorateId == dto.GovernorateId))
        {
            return TypedResults.BadRequest(
                $"City '{dto.Name}' already exists in that governorate.");
        }

        var entity = new City
        {
            Name = dto.Name,
            GovernorateId = dto.GovernorateId
        };
        services.Context.Cities.Add(entity);
        await services.Context.SaveChangesAsync();


        var govName = await services.Context.Governorates
                               .Where(g => g.Id == entity.GovernorateId)
                               .Select(g => g.Name)
                               .FirstAsync();


        var result = new CityDTO(
            entity.Id,
            entity.Name,
            entity.GovernorateId,
            govName
        );

        return TypedResults.Created(
            $"/api/catalog/cities/{entity.Id}",
            result
        );
    }

    public static async Task<Results<Ok<CityDTO>, NotFound, BadRequest<string>>> UpdateCity(
        int id,
        CityDTO dto,
        [AsParameters] CatalogServices services)
    {
        var entity = await services.Context.Cities.FindAsync(id);
        if (entity is null)
            return TypedResults.NotFound();

        if (!await services.Context.Governorates.AnyAsync(g => g.Id == dto.GovernorateId))
            return TypedResults.BadRequest($"Governorate ID {dto.GovernorateId} not found.");

        if (await services.Context.Cities.AnyAsync(c => c.Name == dto.Name && c.GovernorateId == dto.GovernorateId && c.Id != id))
            return TypedResults.BadRequest($"City '{dto.Name}' already exists in that governorate.");

        entity.Name = dto.Name;
        entity.GovernorateId = dto.GovernorateId;
        await services.Context.SaveChangesAsync();

        var gov = await services.Context.Governorates.FindAsync(entity.GovernorateId);
        return TypedResults.Ok(new CityDTO(entity.Id, entity.Name, entity.GovernorateId, gov!.Name));
    }

    public static async Task<Results<Ok<string>, NotFound>> DeleteCity(
        int id,
        [AsParameters] CatalogServices services)
    {
        var entity = await services.Context.Cities.FindAsync(id);
        if (entity is null)
            return TypedResults.NotFound();

        services.Context.Cities.Remove(entity);
        await services.Context.SaveChangesAsync();
        return TypedResults.Ok($"City with ID {id} deleted.");
    }
}

public record CreateCityDTO(
       string Name,
       int GovernorateId
   );