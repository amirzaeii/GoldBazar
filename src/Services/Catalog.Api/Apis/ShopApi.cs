using Microsoft.AspNetCore.Http.HttpResults;
namespace Catalog.Api;

public static class ShopApi
{
    public static IEndpointRouteBuilder MapShopApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/shop").HasApiVersion(1.0);

        api.MapGet("/shops", GetAllShops)
            .WithName("GetAllShops")
            .WithSummary("Get all shops")
            .WithDescription("Retrieves a list of all shops.")
            .WithTags("Shops");

        api.MapGet("/shops/{id:int}", GetShopById)
            .WithName("GetShopById")
            .WithSummary("Get shop by ID")
            .WithDescription("Retrieves detailed information of a shop by its unique identifier.")
            .WithTags("Shops");

        api.MapPost("/shops", AddShop)
            .WithName("AddShop")
            .WithSummary("Add a new shop")
            .WithDescription("Creates a new shop entry.")
            .WithTags("Shops");

        api.MapPut("/shops/{id:int}", UpdateShop)
            .WithName("UpdateShop")
            .WithSummary("Update a shop")
            .WithDescription("Updates the details of an existing shop by ID.")
            .WithTags("Shops");

        api.MapDelete("/shops/{id:int}", DeleteShop)
            .WithName("DeleteShop")
            .WithSummary("Delete a shop")
            .WithDescription("Deletes a shop by its unique identifier.")
            .WithTags("Shops");

        return app;
    }

    public static async Task<Results<Ok<List<Shop>>, NotFound>> GetAllShops([AsParameters] CatalogServices services)
    {
        var shops = await services.Context.Shops.ToListAsync();
        return shops.Any() ? TypedResults.Ok(shops) : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<Shop>, NotFound>> GetShopById(int id, [AsParameters] CatalogServices services)
    {
        var shop = await services.Context.Shops.FirstOrDefaultAsync(s => s.Id == id);
        return shop is not null ? TypedResults.Ok(shop) : TypedResults.NotFound();
    }

    public static async Task<Results<Created<Shop>, BadRequest<string>>> AddShop(
        Shop shop, [AsParameters] CatalogServices services)
    {
        if (await services.Context.Shops.AnyAsync(s => s.Name == shop.Name))
        {
            return TypedResults.BadRequest($"A shop with the name '{shop.Name}' already exists.");
        }

        services.Context.Shops.Add(shop);
        await services.Context.SaveChangesAsync();
        return TypedResults.Created($"/shops/{shop.Id}", shop);
    }

    public static async Task<Results<Ok<Shop>, NotFound, BadRequest<string>>> UpdateShop(
        int id, Shop updatedShop, [AsParameters] CatalogServices services)
    {
        var shop = await services.Context.Shops.FirstOrDefaultAsync(s => s.Id == id);
        if (shop is null)
        {
            return TypedResults.NotFound();
        }

        // Update properties
        shop.Name = updatedShop.Name;
        shop.City = updatedShop.City;
        shop.Address = updatedShop.Address;
        shop.ContactNumber = updatedShop.ContactNumber;
        shop.Owner = updatedShop.Owner;

        await services.Context.SaveChangesAsync();
        return TypedResults.Ok(shop);
    }

    public static async Task<Results<Ok<string>, NotFound>> DeleteShop(
        int id, [AsParameters] CatalogServices services)
    {
        var shop = await services.Context.Shops.FirstOrDefaultAsync(s => s.Id == id);
        if (shop is null)
        {
            return TypedResults.NotFound();
        }

        services.Context.Shops.Remove(shop);
        await services.Context.SaveChangesAsync();
        return TypedResults.Ok($"Shop with ID {id} deleted.");
    }
}
