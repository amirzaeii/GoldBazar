using System.ComponentModel;

using GoldBazar.Shared.DTOs;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

using static Catalog.Infrastructure.Models.Item;

namespace Catalog.Api.Apis;

public static class CatalogApi
{
    public static IEndpointRouteBuilder MapCatalogApiV1(this IEndpointRouteBuilder app)
    {
         var api = app.MapGroup("api/catalog").HasApiVersion(1.0);

        api.MapGet("/Items", GetItemList)
            .WithName("ProductsList")
            .WithSummary("List of products")
            .WithDescription("Get a paginated list of products in the catalog.")
            .WithTags("Items");

        api.MapGet("/items/by", GetItemsByIds)
            .WithName("BatchGetItems")
            .WithSummary("Batch get catalog items")
            .WithDescription("Get multiple items from the catalog")
            .WithTags("Items");

        api.MapGet("/categories", GetCategoryList)
            .WithName("Item Category List")
            .WithSummary("List of item categories")
            .WithDescription("Get list of item categories.")
            .WithTags("Category");     

        api.MapGet("/categories/{id:int}/pic", GetCategoryPictureById)
            .WithName("GetTypePicture")
            .WithSummary("Get catalog type picture")
            .WithDescription("Get the picture for a catalog type")
            .WithTags("Category");      
        
        api.MapGet("/Items/{id:int}", GetItemById)
            .WithName("GetProduct")
            .WithSummary("Get an item by its ID")
            .WithDescription("Retrieves a specific item from the catalog by its unique identifier (ID).")
            .WithTags("Items");

        api.MapGet("/Items/category/{typeid:int}", GetItemsByCategoryId)
            .WithName("GetProductByType")
            .WithSummary("Get items by their category")
            .WithDescription("Retrieves catalog items filtered by their category identifier.")
            .WithTags("Items");


        api.MapGet("/items/{id:int}/pic", GetItemPictureById)
            .WithName("GetItemPicture")
            .WithSummary("Get catalog item picture")
            .WithDescription("Get the picture for a catalog item")
            .WithTags("Items");

        api.MapPost("/item", AddItem)
            .WithName("additem")
            .WithSummary("create an item")
            .WithDescription("create an item")
            .WithTags("items");      

        api.MapPost("/item/pic", UploadItemImage)
            .DisableAntiforgery()
            .WithName("addproductimage")
            .WithSummary("upload an item image")
            .WithDescription("upload an item image")
            .WithTags("items");

        api.MapPut("/item/{id}", UpdateItem)
            .WithName("edititem")
            .WithSummary("Update an item")
            .WithDescription("Update an item")
            .WithTags("Items");

        api.MapDelete("/item/{id}", DeleteItem)
            .WithName("DeleteItem")
            .WithSummary("Delete an item")
            .WithDescription("delete an item")
            .WithTags("Items");

        api.MapGet("/item/similar/{categoryId}", GetSimilarItems)
            .WithName("GetSimilarItems")
            .WithSummary("Get list of similar items by its categoryId")
            .WithDescription("Get list of similar items by its categoryId")
            .WithTags("Items");

        //api.MapPost("/item/filter", FilterByComposite)
        //     .WithName("FilterByComposite")
        //     .WithSummary("Filter catalog items by composite filter")
        //     .WithDescription("Apply various filters to get a list of catalog items along with their corresponding shop details.")
        //     .WithTags("Items");
        //api.MapPost("/item/filter", FilterByComposite)
        // .WithName("FilterByComposite")
        // .WithSummary("Filter catalog items by composite filter")
        // .WithDescription("Apply various filters to get a list of catalog items along with their corresponding shop details.")
        // .WithTags("Items");

        api.MapGet("/items/discounted", GetDiscountedItems)
          .WithName("GetDiscountedItems")
          .WithSummary("List of discounted items")
          .WithDescription("Get a list of all items that have a discount or offer.")
          .WithTags("Items");

        return app;
    }

    public static async Task<Results<Ok<PaginatedItems<ItemDTO>>, BadRequest<string>>> GetItemList(
        [AsParameters] PaginationRequest paginationRequest,
        [AsParameters] CatalogServices services)
    {
        var pageSize = paginationRequest.PageSize;
        var pageIndex = paginationRequest.PageIndex;

        var totalItems = await services.Context.Items
            .LongCountAsync();

        var itemsOnPage = await services.Context.Items
            .Include(i => i.Metal)
            .Include(i => i.Material)
            .Include(i => i.Manufacture)
            .Include(i => i.Style)
            .Include(i => i.Occassion)
            .Include(i => i.Shop)
            .OrderBy(c => c.Caption)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Select(s => new ItemDTO {
                            Id = s.Id,
                            Caption = s.Caption,
                            Description = s.Description ?? string.Empty,
                            CostPerGram = s.CostPerGram,
                            Weight = s.Weight,
                            ManufactureId = s.ManufactureId,
                            ManufactureName = s.Manufacture.Name,
                            TypeId = (int)s.Type,
                            TypeName = s.Type.GetDisplayName(),
                            ChangePriceRange = s.EligibleChangePriceRang,
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
                            MainPhoto = s.MainPhoto ?? "default.png"
                        }).ToArrayAsync();

        return TypedResults.Ok(new PaginatedItems<ItemDTO>(pageIndex, pageSize, totalItems, itemsOnPage));
    }
    public static async Task<Ok<List<ItemDTO>>> GetItemsByIds(
        [AsParameters] CatalogServices services,
        [Description("List of ids for catalog items to return")] int[] ids)
    {
        var items = await services.Context.Items.Where(item => ids.Any(a => a == item.Id))
            .Include(i => i.Metal)
            .Include(i => i.Material)
            .Include(i => i.Manufacture)
            .Include(i => i.Style)
            .Include(i => i.Occassion)
            .Include(i => i.Shop)
            .OrderBy(c => c.Caption)
            .Select(s =>  new ItemDTO {
                            Id = s.Id,
                            Caption = s.Caption,
                            Description = s.Description ?? string.Empty,
                            CostPerGram = s.CostPerGram,
                            Weight = s.Weight,
                            Size = s.Size,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.Name,
                            ManufactureId = s.ManufactureId,
                            ManufactureName = s.Manufacture.Name,
                            TypeId = (int)s.Type,
                            TypeName = s.Type.GetDisplayName(),
                            ChangePriceRange = s.EligibleChangePriceRang,
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
                            MainPhoto = s.MainPhoto ?? "default.png"
            }).ToListAsync();
        return TypedResults.Ok(items);
    }
    public static async Task<Results<Ok<ItemDTO>, NotFound>> GetItemById(
        int id, [AsParameters] CatalogServices services)
    {
        var item = await services.Context.Items.Where(i => i.Id == id)
                .Include(i => i.Metal)
                .Include(i => i.Material)
                .Include(i => i.Manufacture)
                .Include(i => i.Style)
                .Include(i => i.Occassion)
                .Include(i => i.Shop)
                .Select(s => new ItemDTO {
                            Id = s.Id,
                            Caption = s.Caption,
                            Description = s.Description ?? string.Empty,
                            CostPerGram = s.CostPerGram,
                            Weight = s.Weight,
                            Size = s.Size,
                            CategoryId = s.CategoryId,
                            ManufactureId = s.ManufactureId,
                            ManufactureName = s.Manufacture.Name,
                            TypeId = (int)s.Type,
                            TypeName = s.Type.GetDisplayName(),
                            ChangePriceRange = s.EligibleChangePriceRang,
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
                            MainPhoto = s.MainPhoto ?? "default.png"
                }).FirstOrDefaultAsync();

        return item is not null ? TypedResults.Ok(item) : TypedResults.NotFound();
    }
    public static async Task<Results<Ok<ItemDTO[]>, NotFound>> GetItemsByCategoryId(
       int categoryId, [AsParameters] CatalogServices services)
    {
        var items = await services.Context.Items.Where(i => i.CategoryId == categoryId)
            .Include(i => i.Metal)
            .Include(i => i.Material)
            .Include(i => i.Manufacture)
            .Include(i => i.Style)
            .Include(i => i.Occassion)
            .Include(i => i.Shop)            
            .Select(s => new ItemDTO {
                            Id = s.Id,
                            Caption = s.Caption,
                            Description = s.Description ?? string.Empty,
                            CostPerGram = s.CostPerGram,
                            Weight = s.Weight,
                            Size = s.Size,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.Name,
                            ManufactureId = s.ManufactureId,
                            ManufactureName = s.Manufacture.Name,
                            TypeId = (int)s.Type,
                            TypeName = s.Type.GetDisplayName(),
                            ChangePriceRange = s.EligibleChangePriceRang,
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
                            MainPhoto = s.MainPhoto ?? "default.png"
            }).ToArrayAsync();

        return items is not null ? TypedResults.Ok(items) : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<ItemCategoryDTO[]>, BadRequest<string>>> GetCategoryList(
            [AsParameters] CatalogServices services)
        {
            var totalItems = await services.Context.Categories.LongCountAsync();

            var catalogItemTypes = await services.Context.Categories
                .OrderBy(pt => pt.Name)
                .Select(s => new ItemCategoryDTO(s.Id, s.Name, s.Photo)).ToArrayAsync();

            return TypedResults.Ok(catalogItemTypes);
        }

    public static async Task<Results<Ok<ItemDTO>, NotFound>> UpdateItem(
        int id,
        [Description("The catalog item to update")]
        [FromBody]ItemDTO updatedProduct, 
        [AsParameters] CatalogServices services)
    {
        var item = await services.Context.Items.FirstOrDefaultAsync(p => p.Id == id);

        if (item == null)
        {
            return TypedResults.NotFound();
        }

        item.Caption = updatedProduct.Caption;
        item.CostPerGram = updatedProduct.CostPerGram;
        item.Size = updatedProduct.Size;
        item.AvailableStock = updatedProduct.Quantity;
        item.EligibleChangePriceRang = updatedProduct.ChangePriceRange;
        item.ManufactureId = updatedProduct.ManufactureId;
        item.Status = updatedProduct.Status;
        item.Discount = updatedProduct.Discount;
        item.Weight = updatedProduct.Weight;
        item.CategoryId = updatedProduct.CategoryId;
        item.MaterialId = updatedProduct.MaterialId;
        item.Type = (TypeEnum)updatedProduct.TypeId;
        item.MetalId = updatedProduct.MetalId;
        item.OccasionId = updatedProduct.OccasionId;
        item.StyleId = updatedProduct.StyleId;
        item.Description = updatedProduct.Description;

        await services.Context.SaveChangesAsync();

        return TypedResults.Ok(new ItemDTO { Caption = updatedProduct.Caption, Id = updatedProduct.Id });
    }

    public static async Task<Results<Ok<string>, NotFound>> DeleteItem(
        int id, [AsParameters] CatalogServices services)
    {
        var item = await services.Context.Items.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null)
        {
            return TypedResults.NotFound();
        }

        services.Context.Items.Remove(item);
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok($"Item with ID {id} deleted.");
    }

    public static async Task<Ok<List<ItemDTO>>> GetSimilarItems(
     int typeId, [AsParameters] CatalogServices services)
    {
        var similarProducts = await services.Context.Items
            .Where(p => p.CategoryId == typeId)
            .Include(p => p.Metal)
            .Include(p => p.Material)
            .Include(p => p.Manufacture)
            .Include(p => p.Category)
            .Include(p => p.Shop)
            .Include(p => p.Type)
            .Select(s => new ItemDTO {
                            Id = s.Id,
                            Caption = s.Caption,
                            Description = s.Description ?? string.Empty,
                            CostPerGram = s.CostPerGram,
                            Weight = s.Weight,
                            Size = s.Size,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.Name,
                            MetalId = s.MetalId,
                            MetalName = s.Metal.Name,
                            KT = s.Manufacture.Karat,
                            Purity = s.Manufacture.Purity,
                            ManufactureId = s.ManufactureId,
                            ManufactureName = s.Manufacture.Name,
                            TypeId = (int)s.Type,
                            TypeName = s.Type.GetDisplayName(),
                            ChangePriceRange = s.EligibleChangePriceRang,
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
                            MainPhoto = s.MainPhoto ?? "default.png"
            })
            .ToListAsync();

        return TypedResults.Ok(similarProducts);
    }


    [ProducesResponseType<byte[]>(StatusCodes.Status200OK, "application/octet-stream",
        [ "image/png", "image/gif", "image/jpeg", "image/bmp", "image/tiff",
          "image/wmf", "image/jp2", "image/svg+xml", "image/webp" ])]
    public static async Task<Results<PhysicalFileHttpResult,NotFound>> GetItemPictureById(
        CatalogContext context,
        IWebHostEnvironment environment,
        [Description("The catalog item id")] int id)
    {
        var item = await context.Items.FindAsync(id);

        if (item is null)
        {
            return TypedResults.NotFound();
        }

        var path = GetFullPathItem(environment.ContentRootPath, item.MainPhoto ?? "default.png");

        string imageFileExtension = Path.GetExtension(item.MainPhoto ?? "default.png");
        string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);
        DateTime lastModified = File.GetLastWriteTimeUtc(path);

        return TypedResults.PhysicalFile(path, mimetype, lastModified: lastModified);
    }

    [ProducesResponseType<byte[]>(StatusCodes.Status200OK, "application/octet-stream",
        [ "image/png", "image/gif", "image/jpeg", "image/bmp", "image/tiff",
          "image/wmf", "image/jp2", "image/svg+xml", "image/webp" ])]
    public static async Task<Results<Ok<string>,NotFound>> GetCategoryPictureById(
        CatalogContext context,
        IWebHostEnvironment environment,
        [Description("The catalog type id")] int id)
    {
        var item = await context.Categories.FindAsync(id);

        if (item is null)
        {
            return TypedResults.NotFound();
        }
        
        if(item.Photo!.StartsWith("http"))
        {
            return TypedResults.Ok(item.Photo);
        }

        string imageFileExtension = Path.GetExtension(item.Photo ?? "default.png");
        string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

        var path = GetFullPathType(environment.ContentRootPath, item.Photo ?? "default.png");       
        DateTime lastModified = File.GetLastWriteTimeUtc(path);
        //return TypedResults.PhysicalFile(path, mimetype, lastModified: lastModified);
        return TypedResults.Ok(path);
    }
    public static async Task<Results<Ok<List<ItemDTO>>, BadRequest<string>>> GetDiscountedItems(
       [AsParameters] CatalogServices services)
    {
        var discountedProducts = await services.Context.Items
            .Where(p => p.Discount > 0)
            .Include(p => p.Metal)
            .Include(p => p.Material)
            .Include(p => p.Manufacture)
            .Include(p => p.Shop)
            .Include(p => p.Type)
            .Select(s => new ItemDTO {
                            Id = s.Id,
                            Caption = s.Caption,
                            Description = s.Description ?? string.Empty,
                            CostPerGram = s.CostPerGram,
                            Weight = s.Weight,
                            Size = s.Size,
                            CategoryId = s.CategoryId,
                            CategoryName = s.Category.Name,
                            MetalId = s.MetalId,
                            MetalName = s.Metal.Name,
                            KT = s.Manufacture.Karat,
                            Purity = s.Manufacture.Purity,
                            ShopId = s.ShopId,
                            ManufactureId = s.ManufactureId,
                            ManufactureName = s.Manufacture.Name,
                            TypeId = (int)s.Type,
                            TypeName = s.Type.GetDisplayName(),
                            ChangePriceRange = s.EligibleChangePriceRang,
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
                            MainPhoto = s.MainPhoto ?? "default.png"
            })
            .ToListAsync();

        return discountedProducts.Any() ? TypedResults.Ok(discountedProducts) : TypedResults.BadRequest("No discounted products found.");
    }
    ////Added
    //public static async Task<Ok<List<ItemDto>>> FilterByComposite(
    //   [AsParameters] CatalogServices services,
    //   [Description("Composite filter for catalog items")][FromBody] CompositeFilterDto request)
    //{
    //    var products = services.Context.Items
    //        .Include(p => p.Shop)
    //        .Include(p => p.Material)
    //        .Include(p => p.Metal)
    //        .Include(p => p.Style)
    //        .Include(p => p.Occassion)
    //        .Include(p => p.Type)
    //        .AsNoTracking()
    //        .AsQueryable();

    //    // Filter by weight range
    //    if (request.MinWeight > 0 && request.MaxWeight > 0 && request.MinWeight <= request.MaxWeight)
    //    {
    //        products = products.Where(p => p.Weight >= request.MinWeight && p.Weight <= request.MaxWeight);
    //    }

    //    // Filter by product type
    //    if ((request.ProductType ?? Array.Empty<int>()).Any())
    //    {
    //        products = products.Where(p => request.ProductType.Contains(p.TypeId));
    //    }

    //    // Filter by material
    //    if ((request.Material ?? Array.Empty<int>()).Any())
    //    {
    //        products = products.Where(p => request.Material.Contains(p.MaterialId));
    //    }

    //    // Filter by metal
    //    if ((request.Metal ?? Array.Empty<int>()).Any())
    //    {
    //        products = products.Where(p => request.Metal.Contains(p.MetalId));
    //    }

    //    // Filter by occasion
    //    if ((request.Occasion ?? Array.Empty<int>()).Any())
    //    {
    //        products = products.Where(p => request.Occasion.Contains(p.OccasionId));
    //    }

    //    // Filter by style
    //    if ((request.Style ?? Array.Empty<int>()).Any())
    //    {
    //        products = products.Where(p => request.Style.Contains(p.StyleId));
    //    }

    //    // Project and return results as DTOs
    //    var filteredProducts = await products
    //        .Select(p => new ItemDto(
    //            p.Id,                           // int
    //            p.Caption,                      // string
    //            p.Description,                  // string
    //            p.CostPerGram,                  // decimal
    //            p.Weight,                       // decimal
    //            p.Size,                         // int
    //            p.TypeId,                       // int
    //            p.Type.Name,                    // string
    //            p.MetalId,                      // int
    //            p.Metal.Name,                   // string
    //            p.Metal.Karat,                  // decimal
    //            p.ShopId,                       // int
    //            p.Shop.Name,                    // string
    //            p.Shop.City,                    // string (City comes here)
    //            p.MaterialId,                   // int (MaterialId should follow)
    //            p.Material.Name,                // string
    //            p.OccasionId,                   // int
    //            p.Occassion.Name,               // string
    //            p.StyleId,                      // int
    //            p.Style.Name,                   // string
    //            p.Discount,                     // decimal
    //            p.ActivityStatus                // bool (Status)
    //        )).ToListAsync();

    //    return TypedResults.Ok(filteredProducts);
    

    // public static async Task<Ok<List<ItemDto>>> FilterByComposite(
    // [AsParameters] CatalogServices services,
    // [Description("Composite filter for catalog items")][FromBody] CompositeFilterDto request)
    // {
    //     var products = services.Context.Items
    //         .Include(p => p.Material)
    //         .Include(p => p.Metal)
    //         .Include(p => p.Style)
    //         .Include(p => p.Occassion)
    //         .Include(p => p.Type)
    //         .AsNoTracking()
    //         .AsQueryable();

    //     // Apply dynamic filters
    //     if (request.MinWeight > 0 && request.MaxWeight > 0 && request.MinWeight <= request.MaxWeight)
    //     {
    //         products = products.Where(p => p.Weight >= request.MinWeight && p.Weight <= request.MaxWeight);
    //     }

    //     if (request.ProductTypes != null && request.ProductTypes.Any())
    //     {
    //         products = products.Where(p => request.ProductTypes.Contains(p.TypeId));
    //     }

    //     if (request.Materials != null && request.Materials.Any())
    //     {
    //         products = products.Where(p => request.Materials.Contains(p.MaterialId));
    //     }

    //     if (request.Metals != null && request.Metals.Any())
    //     {
    //         products = products.Where(p => request.Metals.Contains(p.MetalId));
    //     }

    //     if (request.Occasions != null && request.Occasions.Any())
    //     {
    //         products = products.Where(p => request.Occasions.Contains(p.OccasionId));
    //     }

    //     if (request.Styles != null && request.Styles.Any())
    //     {
    //         products = products.Where(p => request.Styles.Contains(p.StyleId));
    //     }

    //     var filteredProducts = await products
    //         .Select(p => new ItemDto(
    //             p.Id,
    //             p.Caption,
    //             p.Description,
    //             p.CostPerGram,
    //             p.Weight,
    //             p.Size,
    //             p.TypeId,
    //             p.Type.Name,
    //             p.MetalId,
    //             p.Metal.Name,
    //             p.Metal.Karat,
    //             p.Metal.Purity,
    //             0,
    //             string.Empty,
    //             string.Empty,
    //             p.MaterialId,
    //             p.Material.Name,
    //             p.OccasionId,
    //             p.Occassion.Name,
    //             p.StyleId,
    //             p.Style.Name,
    //             p.Discount,
    //             p.ActivityStatus,
    //             p.Quantity,
    //             p.MainPhoto
    //         )).ToListAsync();

    //     return TypedResults.Ok(filteredProducts);
    // }
    public static async Task<Results<Created<ItemDTO>, BadRequest<string>>> AddItem(
      [FromBody] ItemDTO newItem,
      [AsParameters] CatalogServices services)
    {
        if (newItem.CostPerGram <= 0 || newItem.Weight <= 0)
        {
            return TypedResults.BadRequest("Cost, weight, and quantity must be greater than zero.");
        }

        var item = new Item
        {
            Caption = newItem.Caption,
            Description = newItem.Description,
            CostPerGram = newItem.CostPerGram,
            Weight = newItem.Weight,
            Size = newItem.Size,
            CategoryId = newItem.CategoryId,
            MetalId = newItem.MetalId,
            ShopId = newItem.ShopId,
            MaterialId = newItem.MaterialId,
            OccasionId = newItem.OccasionId,
            StyleId = newItem.StyleId,
            Discount = newItem.Discount,
            Status = newItem.Status,
            AvailableStock = newItem.Quantity,
            ManufactureId = newItem.ManufactureId,
            Type = (TypeEnum)newItem.TypeId,
            MainPhoto = newItem.MainPhoto,
            EligibleChangePriceRang = newItem.ChangePriceRange,
        };

        services.Context.Items.Add(item);
        await services.Context.SaveChangesAsync();

        // Project ItemDto directly from item instance
        var itemDto = new ItemDTO { Caption = item.Caption, Id = item.Id };

        return TypedResults.Created($"/api/catalog/items/{item.Id}", itemDto);
    }

     public static async Task<Results<Created<string>, BadRequest<string>>> UploadItemImage(
     [FromForm]IFormFile file, 
     IStorageService services)
        {
            if (file == null || file.Length == 0)
                return TypedResults.BadRequest("File is empty.");

            var result = await services.UploadFileAsync(file);

            return TypedResults.Created($"wwwroot/uploads/items/{result}", result);
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

