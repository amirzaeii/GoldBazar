using System.ComponentModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api;

public static class CatalogApi
{
    public static IEndpointRouteBuilder MapCatalogApiV1(this IEndpointRouteBuilder app)
    {
         var api = app.MapGroup("api/catalog").HasApiVersion(1.0);

        api.MapGet("/Items", GetCatalogItemList)
            .WithName("ProductsList")
            .WithSummary("List of products")
            .WithDescription("Get a paginated list of products in the catalog.")
            .WithTags("Items");
        
        api.MapGet("/types", GetCatalogTypeList)
            .WithName("cCtalog Type List")
            .WithSummary("List of catalog types")
            .WithDescription("Get list of catalog type.")
            .WithTags("Types");

        api.MapGet("/types/{id:int}/pic", GetTypePictureById)
            .WithName("GetTypePicture")
            .WithSummary("Get catalog type picture")
            .WithDescription("Get the picture for a catalog type")
            .WithTags("Types");

        api.MapGet("/Items/{id}", GetCatalogItemById)
            .WithName("GetProduct")
            .WithSummary("Get a item by its id")
            .WithDescription("Get a item item by its id")
            .WithTags("Items");

        api.MapGet("/items/{id:int}/pic", GetItemPictureById)
            .WithName("GetItemPicture")
            .WithSummary("Get catalog item picture")
            .WithDescription("Get the picture for a catalog item")
            .WithTags("Items");  

        api.MapPost("/item", AddProduct)
            .WithName("AddProduct")
            .WithSummary("Create a item")
            .WithDescription("reate a item")
            .WithTags("Items");

        api.MapPut("/item/{id}", UpdateProduct)
            .WithName("EditProduct")
            .WithSummary("Update a item")
            .WithDescription("Update a item")
            .WithTags("Items");

        api.MapDelete("/item/{id}", DeleteProduct)
            .WithName("DeleteProduct")
            .WithSummary("Delete a item")
            .WithDescription("delete a item")
            .WithTags("Items");

        api.MapGet("/item/similar/{typeId}", GetSimilarProducts)
            .WithName("GetSimilarProducts")
            .WithSummary("Get list of similar products")
            .WithDescription("Get list of similar products")
            .WithTags("Items");

        api.MapGet("/item/filter", FilterProducts)
            .WithName("FilterProducts")
            .WithSummary("Filter products")
            .WithDescription("Filter products")
            .WithTags("Items");

        return app;
    }

    public static async Task<Results<Ok<PaginatedItems<ItemDto>>, BadRequest<string>>> GetCatalogItemList(
        [AsParameters] PaginationRequest paginationRequest,
        [AsParameters] CatalogServices services)
    {
        var pageSize = paginationRequest.PageSize;
        var pageIndex = paginationRequest.PageIndex;

        var totalItems = await services.Context.Items
            .LongCountAsync();

        var itemsOnPage = await services.Context.Items.Include(i => i.Metal).Include(i => i.Material).Include(i => i.Shop)
            .OrderBy(c => c.Caption)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Select(s => new ItemDto (s.Id, s.Caption, s.Description, 
                            s.CostPerGram, s.Weight, s.Size, 
                            s.TypeId, s.Type.Name, s.MetalId, s.Metal.Name, s.Metal.Karat, 
                            s.ShopId, s.Shop.Name, s.MaterialId, s.Material.Name, s.OccasionId, s.Occassion.Name
                            ,s.StyleId, s.Style.Name, s.Discount, s.ActivityStatus)).ToArrayAsync();

        return TypedResults.Ok(new PaginatedItems<ItemDto>(pageIndex, pageSize, totalItems, itemsOnPage));
    }

    public static async Task<Results<Ok<Item>, NotFound>> GetCatalogItemById(
        int id, [AsParameters] CatalogServices services)
    {
        var item = await services.Context.Items.FirstOrDefaultAsync(p => p.Id == id);
        return item is not null ? TypedResults.Ok(item) : TypedResults.NotFound();
    }

     public static async Task<Results<Ok<TypeDto[]>, BadRequest<string>>> GetCatalogTypeList(
            [AsParameters] CatalogServices services)
        {
            var totalItems = await services.Context.Types.LongCountAsync();

            var catalogItemTypes = await services.Context.Types
                .OrderBy(pt => pt.Name)
                .Select(s => new TypeDto(s.Name, s.Id, s.Photo)).ToArrayAsync();

            return TypedResults.Ok(catalogItemTypes);
        }

    public static async Task<Results<Created<ItemDto>, BadRequest<string>>> AddProduct(
        ItemDto item, [AsParameters] CatalogServices services)
    {
        if (await services.Context.Items.AnyAsync(p => p.Id == item.Id))
        {
            return TypedResults.BadRequest($"A item with ID {item.Id} already exists.");
        }

        services.Context.Items.Add(new Item{ 
            Caption = item.Caption,
            ActivityStatus = item.Status,
            Discount = item.Discount,
            Weight = item.Weight,
            TypeId = item.TypeId,
            MaterialId = item.MaterialId,
            MetalId = item.MetalId,
            OccasionId = item.OccasionId,
            StyleId = item.StyleId,
            Description = item.Description
        });
        await services.Context.SaveChangesAsync();

        return TypedResults.Created($"/item/{item.Id}", item);
    }

    public static async Task<Results<Ok<Item>, NotFound>> UpdateProduct(
        int id, Item updatedProduct, [AsParameters] CatalogServices services)
    {
        var item = await services.Context.Items.FirstOrDefaultAsync(p => p.Id == id);

        if (item == null)
        {
            return TypedResults.NotFound();
        }

        item.Caption = updatedProduct.Caption;
        item.ActivityStatus = updatedProduct.ActivityStatus;
        item.Discount = updatedProduct.Discount;
        item.Weight = updatedProduct.Weight;
        item.TypeId = updatedProduct.TypeId;
        item.MaterialId = updatedProduct.MaterialId;
        item.MetalId = updatedProduct.MetalId;
        item.OccasionId = updatedProduct.OccasionId;
        item.StyleId = updatedProduct.StyleId;
        item.Description = updatedProduct.Description;

        await services.Context.SaveChangesAsync();

        return TypedResults.Ok(item);
    }

    public static async Task<Results<Ok<string>, NotFound>> DeleteProduct(
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

    public static async Task<Ok<List<Item>>> GetDiscountedProducts(
        [AsParameters] CatalogServices services)
    {
        var discountedProducts = await services.Context.Items
            .Where(p => p.Discount > 0)
            .ToListAsync();

        return TypedResults.Ok(discountedProducts);
    }

    public static async Task<Ok<List<Item>>> GetSimilarProducts(
        int typeId, [AsParameters] CatalogServices services)
    {
        var similarProducts = await services.Context.Items
            .Where(p => p.TypeId == typeId)
            .ToListAsync();

        return TypedResults.Ok(similarProducts);
    }

    public static async Task<Ok<List<Item>>> FilterProducts(
        [AsParameters] CatalogServices services,
        [Description("object filter composit")][FromBody] CompositeFilterDto filter
        )
    {
        var queryableProducts = services.Context.Items.AsQueryable();

        if (filter.MinWeight > 0 || filter.MaxWeight > 0)
        {
            queryableProducts = queryableProducts.Where(p =>
                (filter.MinWeight == 0 || p.Weight >= filter.MinWeight) &&
                (filter.MaxWeight == 0 || p.Weight <= filter.MaxWeight));
        }

        if (!string.IsNullOrWhiteSpace(filter.ProductType))
        {
            var productTypes = filter.ProductType.Split(',').Select(pt => pt.Trim()).ToList();
            queryableProducts = queryableProducts.Where(p =>
                productTypes.Any(pt => string.Equals(pt, p.Type.Name, StringComparison.OrdinalIgnoreCase)));
        }

        if (!string.IsNullOrWhiteSpace(filter.Material))
        {
            var materials = filter.Material.Split(',').Select(m => m.Trim()).ToList();
            queryableProducts = queryableProducts.Where(p =>
                materials.Any(m => string.Equals(m, p.Material.Name, StringComparison.OrdinalIgnoreCase)));
        }

        if (!string.IsNullOrWhiteSpace(filter.Metal))
        {
            var metals = filter.Metal.Split(',').Select(m => m.Trim()).ToList();
            queryableProducts = queryableProducts.Where(p =>
                metals.Any(m => string.Equals(m, p.Metal.Name, StringComparison.OrdinalIgnoreCase)));
        }

        if (!string.IsNullOrWhiteSpace(filter.Size))
        {
            var sizes = filter.Size.Split(',').Select(int.Parse).ToList();
            queryableProducts = queryableProducts.Where(p => sizes.Contains(p.Size));
        }

        if (!string.IsNullOrWhiteSpace(filter.Occasion))
        {
            var occasions = filter.Occasion.Split(',').Select(o => o.Trim()).ToList();
            queryableProducts = queryableProducts.Where(p =>
                occasions.Any(o => string.Equals(o, p.Occassion.Name, StringComparison.OrdinalIgnoreCase)));
        }

        if (!string.IsNullOrWhiteSpace(filter.Style))
        {
            var styles = filter.Style.Split(',').Select(s => s.Trim()).ToList();
            queryableProducts = queryableProducts.Where(p =>
                styles.Any(s => string.Equals(s, p.Style.Name, StringComparison.OrdinalIgnoreCase)));
        }

        //if (!string.IsNullOrWhiteSpace(filter.Manufacturer))
        //{
        //    var manufacturers = filter.Metal.Manufacturer.Split(',').Select(m => m.Trim()).ToList();
        //    queryableProducts = queryableProducts.Where(p =>
        //        manufacturers.Any(m => string.Equals(m, p.Manufacturer, StringComparison.OrdinalIgnoreCase)));
        //}

        var filteredProducts = await queryableProducts.ToListAsync();

        return TypedResults.Ok(filteredProducts);
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
    public static async Task<Results<PhysicalFileHttpResult,NotFound>> GetTypePictureById(
        CatalogContext context,
        IWebHostEnvironment environment,
        [Description("The catalog type id")] int id)
    {
        var item = await context.Types.FindAsync(id);

        if (item is null)
        {
            return TypedResults.NotFound();
        }

        var path = GetFullPathType(environment.ContentRootPath, item.Photo ?? "default.png");

        string imageFileExtension = Path.GetExtension(item.Photo ?? "default.png");
        string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);
        DateTime lastModified = File.GetLastWriteTimeUtc(path);

        return TypedResults.PhysicalFile(path, mimetype, lastModified: lastModified);
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
        Path.Combine(contentRootPath, "Pic/Type", pictureFileName);
    public static string GetFullPathItem(string contentRootPath, string pictureFileName) =>
        Path.Combine(contentRootPath, "Pic/Item", pictureFileName);
}