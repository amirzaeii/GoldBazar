using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api;

public static class CatalogApi

{
    public static IEndpointRouteBuilder MapCatalogApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/catalog").HasApiVersion(1.0);

        // Routes for managing products
        api.MapGet("/products", GetAllProducts);
        api.MapGet("/product/{id:int}", GetProductById);
        api.MapPost("/products", CreateProduct);
        api.MapPut("/products/{id:int}", UpdateProduct);
        api.MapDelete("/products/{id:int}", DeleteProduct);

        return app;
    }

    public static async Task<Results<Ok<List<ProductDto>>, BadRequest<string>>> GetAllProducts(
        CatalogContext context)
    {
        var productDtos = await context.Products.Select(s =>new ProductDto(s)).ToListAsync();
        return TypedResults.Ok(productDtos);
    }

    public static async Task<Results<Ok<ProductViewModel>, NotFound>> GetProductById(
        CatalogContext context, IMapper mapper, int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
            return TypedResults.NotFound();

        var productDto = mapper.Map<ProductViewModel>(product);
        return TypedResults.Ok(productDto);
    }

    public static async Task<Results<Created, BadRequest<string>>> CreateProduct(
        CatalogContext context, IMapper mapper, ProductViewModel productDto)
    {
        var product = mapper.Map<Product>(productDto);
        context.Products.Add(product);
        await context.SaveChangesAsync();

        return TypedResults.Created($"/api/catalog/products/{product.Id}");
    }

    public static async Task<Results<NoContent, NotFound, BadRequest<string>>> UpdateProduct(
        CatalogContext context, IMapper mapper, int id, ProductViewModel productDto)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
            return TypedResults.NotFound();

        mapper.Map(productDto, product);
        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> DeleteProduct(
    CatalogContext context, int id)
    {
       
        var product = await context.Products.FindAsync(id);

        if (product == null)
            return TypedResults.NotFound();

        context.Products.Remove(product);

        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }


}