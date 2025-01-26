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

        api.MapGet("/shops/city/{city}", GetShopsByCity)
           .WithName("GetShopsByCity")
           .WithSummary("Get shops by city")
           .WithDescription("Retrieves a list of shops located in the specified city.")
           .WithTags("Shops");

        api.MapGet("/shops/{id:int}/products", GetProductsByShopId)
         .WithName("GetProductsByShopId")
         .WithSummary("Get products by shop ID")
         .WithDescription("Retrieves all products that belong to a specific shop by its unique identifier.")
        .WithTags("Shops");

        api.MapGet("/shops/{id:int}/categories", GetItemCategoriesByShopId)
    .WithName("GetItemCategoriesByShopId")
    .WithSummary("Get item categories by shop ID")
    .WithDescription("Retrieves a list of unique item categories (types) registered for a specific shop by its unique identifier.")
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
    public static async Task<Results<Ok<List<Shop>>, NotFound>> GetShopsByCity(
     string city, [AsParameters] CatalogServices services)
    {
        var shops = await services.Context.Shops
            .Where(s => s.City == city)
            .ToListAsync();

        return shops.Any() ? TypedResults.Ok(shops) : TypedResults.NotFound();
    }
    public static async Task<Results<Ok<List<ItemDto>>, NotFound>> GetProductsByShopId(
    int id, [AsParameters] CatalogServices services)
    {
        var products = await services.Context.Items
            .Include(p => p.Metal)
            .Include(p => p.Material)
            .Include(p => p.Type)
            .Include(p => p.Occassion)
            .Include(p => p.Style)
            .Where(p => p.ShopId == id)
            .Select(p => new ItemDto(
                p.Id,                           // int
                p.Caption,                      // string
                p.Description,                  // string
                p.CostPerGram,                  // decimal
                p.Weight,                       // decimal
                p.Size,                         // int
                p.TypeId,                       // int
                p.Type.Name,                    // string
                p.MetalId,                      // int
                p.Metal.Name,                   // string
                p.Metal.Karat,                  // decimal
                p.Metal.Purity,
                p.ShopId,                       // int
                p.Shop.Name,                    // string
                p.Shop.City,                    // string
                p.MaterialId,                   // int
                p.Material.Name,                // string
                p.OccasionId,                   // int
                p.Occassion.Name,               // string
                p.StyleId,                      // int
                p.Style.Name,                   // string
                p.Discount,                     // decimal
                p.ActivityStatus                // bool
            ))
            .ToListAsync();

        return products.Any()
            ? TypedResults.Ok(products)
            : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<List<TypeDto>>, NotFound>> GetItemCategoriesByShopId(
    int id, [AsParameters] CatalogServices services)
    {
        var categories = await services.Context.Items
            .Where(p => p.ShopId == id) // Filter by ShopId
            .Select(p => p.Type)        // Select the Type (category)
            .Distinct()                 // Ensure no duplicates
            .Select(t => new TypeDto(t.Name, t.Id, t.Photo)) // Map to DTO
            .ToListAsync();

        return categories.Any()
            ? TypedResults.Ok(categories)
            : TypedResults.NotFound();
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
