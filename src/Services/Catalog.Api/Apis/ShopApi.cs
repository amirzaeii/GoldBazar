using GoldBazar.Shared.DTOs;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
namespace Catalog.Api;

public static class ShopApi
{
    public static IEndpointRouteBuilder MapShopApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api").HasApiVersion(1.0);

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

        api.MapGet("/shops/{id:int}/items", GetItemByShop)
         .WithName("GetProductsByShopId")
         .WithSummary("Get products by shop ID")
         .WithDescription("Retrieves all products that belong to a specific shop by its unique identifier.")
         .WithTags("Shops");

        api.MapGet("/shops/{id:int}/logo", GetShopLogo)
         .WithName("GetShopLogo")
         .WithSummary("Get shop logo")
         .WithDescription("get shop logo")
         .WithTags("Shops");

        api.MapGet("/shops/{id:int}/banner", GetShopBanner)
         .WithName("GetShopBanner")
         .WithSummary("Get shop banner")
         .WithDescription("get shop banner")
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

    public static async Task<Results<Ok<ShopDTO[]>, NotFound>> GetAllShops([AsParameters] CatalogServices services)
    {
        var shops = await services.Context.Shops.Select(s => new ShopDTO
        {
            Id = s.Id,
            Name = s.Name,
            CityId = s.CityId,
            CityName = s.City.Name,
            Address = s.Address,
            ContactNo = s.ContactNumber,
            OwnerName = s.Owner,
            LogoUrl = s.Logo,
            BannerUrl = s.Banner,

        }).ToArrayAsync();
        return shops.Length != 0 ? TypedResults.Ok(shops) : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<ShopDTO>, NotFound>> GetShopById(int id, [AsParameters] CatalogServices services)
    {
        var shop = await services.Context.Shops.Where(w => w.Id == id)
        .Select(s => new ShopDTO
        {
            Id = s.Id,
            Name = s.Name,
            CityId = s.CityId,
            CityName = s.City.Name,
            Address = s.Address,
            ContactNo = s.ContactNumber,
            OwnerName = s.Owner,
            LogoUrl = s.Logo,
            BannerUrl = s.Banner,

        }).FirstOrDefaultAsync();
        return shop is not null ? TypedResults.Ok(shop) : TypedResults.NotFound();
    }
    public static async Task<Results<Ok<ShopDTO[]>, NotFound>> GetShopsByCity(
     int cityId, [AsParameters] CatalogServices services)
    {
        var shops = await services.Context.Shops
            .Where(s => s.CityId == cityId)
            .Select(s => new ShopDTO
            {
                Id = s.Id,
                Name = s.Name,
                CityId = s.CityId,
                CityName = s.City.Name,
                Address = s.Address,
                ContactNo = s.ContactNumber,
                OwnerName = s.Owner,
                LogoUrl = s.Logo,
                BannerUrl = s.Banner,

            }).ToArrayAsync();

        return shops.Any() ? TypedResults.Ok(shops) : TypedResults.NotFound();
    }
    public static async Task<Results<Ok<ItemDTO[]>, NotFound>> GetItemByShop(
    int id, 
    [AsParameters] CatalogServices services)
    {
        var products = await services.Context.Items
            .Include(p => p.Metal)
            .Include(p => p.Material)
            .Include(p => p.Type)
            .Include(p => p.Occassion)
            .Include(p => p.Manufacture)
            .Include(p => p.Style)
            .Where(p => p.ShopId == id)
            .Select(s => new ItemDTO {
                            Id = s.Id,
                            Caption = s.Caption,
                            Description = s.Description ?? string.Empty,
                            CostPerGram = s.CostPerGram,
                            Weight = s.Weight,
                            ChangePriceRange = s.EligibleChangePriceRang,
                            ManufactureId = s.ManufactureId,
                            ManufactureName = s.Manufacture.Name,
                            TypeId = (int)s.Type,
                            TypeName = s.Type.GetDisplayName(),
                            Size = s.Size,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.Name,
                            MetalId = s.MetalId,
                            MetalName = s.Metal.Name,
                            KT = s.Manufacture.Karat,
                            Purity = s.Manufacture.Purity,
                            ShopId = s.ShopId,
                            ShopName = s.Shop.Name,
                            City = s.Shop.City.Name,
                            MaterialId = s.MaterialId,
                            MaterialName = s.Material.Name,
                            OccasionId = s.OccasionId,
                            OccasionName = s.Occassion.Name,
                            StyleId = s.StyleId,
                            StyleName = s.Style.Name,
                            Discount = s.Discount,
                            Status = s.Status,
                            Quantity = s.AvailableStock, 
                            MainPhoto = s.MainPhoto ?? string.Empty
            }).ToArrayAsync();

        return products.Any()
            ? TypedResults.Ok(products)
            : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<ItemDTO[]>, NotFound>> GetItemCategoriesByShopId(
    int id, 
    [AsParameters] CatalogServices services)
    {
        var categories = await services.Context.Items
        .Where(p => p.ShopId == id)
        .Select(s => new ItemDTO {
                            Id = s.Id,
                            Caption = s.Caption,
                            Description = s.Description ?? string.Empty,
                            CostPerGram = s.CostPerGram,
                            Weight = s.Weight,
                            ChangePriceRange = s.EligibleChangePriceRang,
                            ManufactureId = s.ManufactureId,
                            ManufactureName = s.Manufacture.Name,
                            TypeId = (int)s.Type,
                            TypeName = s.Type.GetDisplayName(),
                            Size = s.Size,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.Name,
                            MetalId = s.MetalId,
                            MetalName = s.Metal.Name,
                            KT = s.Manufacture.Karat,
                            Purity = s.Manufacture.Purity,
                            ShopId = s.ShopId,
                            ShopName = s.Shop.Name,
                            City = s.Shop.City.Name,
                            MaterialId = s.MaterialId,
                            MaterialName = s.Material.Name,
                            OccasionId = s.OccasionId,
                            OccasionName = s.Occassion.Name,
                            StyleId = s.StyleId,
                            StyleName = s.Style.Name,
                            Discount = s.Discount,
                            Status = s.Status,
                            Quantity = s.AvailableStock, 
                            MainPhoto = s.MainPhoto ?? string.Empty
                                     }).ToArrayAsync();


        return categories.Any()
            ? TypedResults.Ok(categories)
            : TypedResults.NotFound();
    }

    public static async Task<Results<Created<ShopDTO>, BadRequest<string>>> AddShop(
        [FromBody] ShopDTO shop, 
        [AsParameters] CatalogServices services)
    {
        if (await services.Context.Shops.AnyAsync(s => s.Name == shop.Name))
        {
            return TypedResults.BadRequest($"A shop with the name '{shop.Name}' already exists.");
        }

        services.Context.Shops.Add(new Shop
        {
            Name = shop.Name,
            CityId = shop.CityId,
            Address = shop.Address,
            ContactNumber = shop.ContactNo,
            Owner = shop.OwnerName,
            Logo = shop.LogoUrl,
            Banner = shop.BannerUrl
        });
        await services.Context.SaveChangesAsync();
        return TypedResults.Created($"/shops/{shop.Id}", shop);
    }

    public static async Task<Results<Ok<ShopDTO>, NotFound, BadRequest<string>>> UpdateShop(
        int id, 
        [FromBody] ShopDTO updatedShop, 
        [AsParameters] CatalogServices services)
    {
        var shop = await services.Context.Shops.FirstOrDefaultAsync(s => s.Id == id);
        if (shop is null)
        {
            return TypedResults.NotFound();
        }

        // Update properties
        shop.Name = updatedShop.Name;
        shop.CityId = updatedShop.CityId;
        shop.Address = updatedShop.Address;
        shop.ContactNumber = updatedShop.ContactNo;
        shop.Owner = updatedShop.OwnerName;

        await services.Context.SaveChangesAsync();
        return TypedResults.Ok(updatedShop);
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
    
     [ProducesResponseType<byte[]>(StatusCodes.Status200OK, "application/octet-stream",
        [ "image/png", "image/gif", "image/jpeg", "image/bmp", "image/tiff",
          "image/wmf", "image/jp2", "image/svg+xml", "image/webp" ])]
    public static async Task<Results<Ok<string>,NotFound>> GetShopLogo(
        CatalogContext context,
        IWebHostEnvironment environment,
        int id)
    {
        var shop = await context.Shops.FindAsync(id);

        if (shop is null)
        {
            return TypedResults.NotFound();
        }
        
        if(shop.Logo.StartsWith("http"))
        {
            return TypedResults.Ok(shop.Logo);            
        }

        string imageFileExtension = Path.GetExtension(shop.Logo ?? "default.png");
        string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

        var path = GetFullPathType(environment.ContentRootPath, shop.Logo ?? "default.png");       
        DateTime lastModified = File.GetLastWriteTimeUtc(path);
        //return TypedResults.PhysicalFile(path, mimetype, lastModified: lastModified);
        return TypedResults.Ok(path);
    }

      public static async Task<Results<Ok<string>,NotFound>> GetShopBanner(
        CatalogContext context,
        IWebHostEnvironment environment,
        int id)
    {
        var shop = await context.Shops.FindAsync(id);

        if (shop is null)
        {
            return TypedResults.NotFound();
        }
        
        if(shop.Banner!.StartsWith("http"))
        {
            return TypedResults.Ok(shop.Banner);
        }

        string imageFileExtension = Path.GetExtension(shop.Banner ?? "default.png");
        string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

        var path = GetFullPathType(environment.ContentRootPath, shop.Banner ?? "default.png");       
        DateTime lastModified = File.GetLastWriteTimeUtc(path);
        //return TypedResults.PhysicalFile(path, mimetype, lastModified: lastModified);
        return TypedResults.Ok(path);
    }

    private static string GetImageMimeTypeFromImageFileExtension(string extension) => extension switch
    {
        ".png" => "image/png",
        ".gif" => "image/gif",
        ".jpg" or ".jpeg" => "image/jpeg",
        ".bmp" => "image/bmp",
        ".tiff" => "image/tiff",
        ".wmf" => "image/wmf",
        ".jp2" => "image/jp2",
        ".svg" => "image/svg+xml",
        ".webp" => "image/webp",
        _ => "application/octet-stream",
    };

    public static string GetFullPathType(string contentRootPath, string pictureFileName) =>
        Path.Combine(contentRootPath, "wwwroot/uploads/types", pictureFileName);
    public static string GetFullPathItem(string contentRootPath, string pictureFileName) =>
        Path.Combine(contentRootPath, "wwwroot/uploads/items", pictureFileName);

}
