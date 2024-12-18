using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api.Apis
{
    public static class ProductTypeApi
    {
        public static IEndpointRouteBuilder MapProductTypeApi(this IEndpointRouteBuilder app)
        {
            app.MapGet("/producttypes", GetAllProductTypes);
            app.MapGet("/producttypes/{id}", GetProductTypeById);
            app.MapPost("/producttypes", AddProductType);
            app.MapPut("/producttypes/{id}", UpdateProductType);
            app.MapDelete("/producttypes/{id}", DeleteProductType);

            return app;
        }

        public static async Task<Results<Ok<PaginatedItems<ProductType>>, BadRequest<string>>> GetAllProductTypes(
            [AsParameters] PaginationRequest paginationRequest,
            [AsParameters] CatalogServices services)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = await services.Context.ProductTypes.LongCountAsync();

            var itemsOnPage = await services.Context.ProductTypes
                .OrderBy(pt => pt.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return TypedResults.Ok(new PaginatedItems<ProductType>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        public static async Task<Results<Ok<ProductType>, NotFound>> GetProductTypeById(
            int id, [AsParameters] CatalogServices services)
        {
            var productType = await services.Context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);
            return productType is not null ? TypedResults.Ok(productType) : TypedResults.NotFound();
        }

        public static async Task<Results<Created<ProductType>, BadRequest<string>>> AddProductType(
            ProductType productType, [AsParameters] CatalogServices services)
        {
            if (string.IsNullOrWhiteSpace(productType.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            services.Context.ProductTypes.Add(productType);
            await services.Context.SaveChangesAsync();

            return TypedResults.Created($"/producttypes/{productType.Id}", productType);
        }

        public static async Task<Results<Ok<ProductType>, NotFound, BadRequest<string>>> UpdateProductType(
            int id, ProductType updatedProductType, [AsParameters] CatalogServices services)
        {
            var existingProductType = await services.Context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);
            if (existingProductType is null)
            {
                return TypedResults.NotFound();
            }

            if (string.IsNullOrWhiteSpace(updatedProductType.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            existingProductType.Name = updatedProductType.Name;

            await services.Context.SaveChangesAsync();

            return TypedResults.Ok(existingProductType);
        }

        public static async Task<Results<Ok<string>, NotFound>> DeleteProductType(
            int id, [AsParameters] CatalogServices services)
        {
            var productType = await services.Context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);
            if (productType is null)
            {
                return TypedResults.NotFound();
            }

            services.Context.ProductTypes.Remove(productType);
            await services.Context.SaveChangesAsync();

            return TypedResults.Ok($"ProductType with ID {id} deleted.");
        }
    }
}