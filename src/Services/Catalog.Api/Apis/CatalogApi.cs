using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api;

public static class CatalogApi
{
    public static IEndpointRouteBuilder MapCatalogApiV1(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products", GetAllItems);
        app.MapGet("/products/{id}", GetProductById);
        app.MapPost("/products", AddProduct);
        app.MapPut("/products/{id}", UpdateProduct);
        app.MapDelete("/products/{id}", DeleteProduct);
        app.MapGet("/products/discounted", GetDiscountedProducts);
        app.MapGet("/products/similar/{typeId}", GetSimilarProducts);
        app.MapPost("/products/filter", FilterProducts);

        return app;
    }

    public static async Task<Results<Ok<PaginatedItems<Product>>, BadRequest<string>>> GetAllItems(
        [AsParameters] PaginationRequest paginationRequest,
        [AsParameters] CatalogServices services)
    {
        var pageSize = paginationRequest.PageSize;
        var pageIndex = paginationRequest.PageIndex;

        var totalItems = await services.Context.Products
            .LongCountAsync();

        var itemsOnPage = await services.Context.Products
            .OrderBy(c => c.Caption)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return TypedResults.Ok(new PaginatedItems<Product>(pageIndex, pageSize, totalItems, itemsOnPage));
    }

    public static async Task<Results<Ok<Product>, NotFound>> GetProductById(
        int id, [AsParameters] CatalogServices services)
    {
        var product = await services.Context.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product is not null ? TypedResults.Ok(product) : TypedResults.NotFound();
    }

    public static async Task<Results<Created<Product>, BadRequest<string>>> AddProduct(
        Product product, [AsParameters] CatalogServices services)
    {
        if (await services.Context.Products.AnyAsync(p => p.Id == product.Id))
        {
            return TypedResults.BadRequest($"A product with ID {product.Id} already exists.");
        }

        services.Context.Products.Add(product);
        await services.Context.SaveChangesAsync();

        return TypedResults.Created($"/products/{product.Id}", product);
    }

    public static async Task<Results<Ok<Product>, NotFound>> UpdateProduct(
        int id, Product updatedProduct, [AsParameters] CatalogServices services)
    {
        var product = await services.Context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return TypedResults.NotFound();
        }

        product.Caption = updatedProduct.Caption;
        product.ActivityStatus = updatedProduct.ActivityStatus;
        product.HasOffer = updatedProduct.HasOffer;
        product.Discount = updatedProduct.Discount;
        product.Weight = updatedProduct.Weight;
        product.TypeId = updatedProduct.TypeId;
        product.MaterialId = updatedProduct.MaterialId;
        product.MetalId = updatedProduct.MetalId;
        product.OccasionId = updatedProduct.OccasionId;
        product.StyleId = updatedProduct.StyleId;
        product.ShopId = updatedProduct.ShopId;
        product.Description = updatedProduct.Description;

        await services.Context.SaveChangesAsync();

        return TypedResults.Ok(product);
    }

    public static async Task<Results<Ok<string>, NotFound>> DeleteProduct(
        int id, [AsParameters] CatalogServices services)
    {
        var product = await services.Context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return TypedResults.NotFound();
        }

        services.Context.Products.Remove(product);
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok($"Product with ID {id} deleted.");
    }

    public static async Task<Ok<List<Product>>> GetDiscountedProducts(
        [AsParameters] CatalogServices services)
    {
        var discountedProducts = await services.Context.Products
            .Where(p => p.HasOffer && p.Discount > 0)
            .ToListAsync();

        return TypedResults.Ok(discountedProducts);
    }

    public static async Task<Ok<List<Product>>> GetSimilarProducts(
        int typeId, [AsParameters] CatalogServices services)
    {
        var similarProducts = await services.Context.Products
            .Where(p => p.TypeId == typeId)
            .ToListAsync();

        return TypedResults.Ok(similarProducts);
    }

    public static async Task<Ok<List<Product>>> FilterProducts(
        CompositeFilterDto filter, [AsParameters] CatalogServices services)
    {
        var queryableProducts = services.Context.Products.AsQueryable();

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
}