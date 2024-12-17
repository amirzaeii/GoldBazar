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

        private static readonly List<ProductType> productTypes = new();

        public static async Task<Results<Ok<PaginatedItems<ProductType>>, BadRequest<string>>> GetAllProductTypes(
            [AsParameters] PaginationRequest paginationRequest)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = productTypes.Count;

            var itemsOnPage = productTypes
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            return TypedResults.Ok(new PaginatedItems<ProductType>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        public static async Task<Results<Ok<ProductType>, NotFound>> GetProductTypeById(
            int id)
        {
            var productType = productTypes.FirstOrDefault(pt => pt.Id == id);
            return productType is not null ? TypedResults.Ok(productType) : TypedResults.NotFound();
        }

        public static async Task<Results<Created<ProductType>, BadRequest<string>>> AddProductType(
            ProductType productType)
        {
            if (string.IsNullOrWhiteSpace(productType.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            productType.Id = productTypes.Any() ? productTypes.Max(pt => pt.Id) + 1 : 1;
            productTypes.Add(productType);

            return TypedResults.Created($"/producttypes/{productType.Id}", productType);
        }

        public static async Task<Results<Ok<ProductType>, NotFound, BadRequest<string>>> UpdateProductType(
            int id, ProductType updatedProductType)
        {
            var existingProductType = productTypes.FirstOrDefault(pt => pt.Id == id);
            if (existingProductType is null)
            {
                return TypedResults.NotFound();
            }

            if (string.IsNullOrWhiteSpace(updatedProductType.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            existingProductType.Name = updatedProductType.Name;

            return TypedResults.Ok(existingProductType);
        }

        public static async Task<Results<Ok<string>, NotFound>> DeleteProductType(
            int id)
        {
            var productType = productTypes.FirstOrDefault(pt => pt.Id == id);
            if (productType is null)
            {
                return TypedResults.NotFound();
            }

            productTypes.Remove(productType);
            return TypedResults.Ok($"ProductType with ID {id} deleted.");
        }
    }
}