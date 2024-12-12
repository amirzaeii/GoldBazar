namespace Catalog.Api.Apis
{
    public static class ProductTypeApi
    {
        private static readonly List<ProductType> productTypes = new();

        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/producttypes", GetAllProductTypes); // Supports paging
            app.MapGet("/producttypes/{id}", GetProductTypeById);
            app.MapPost("/producttypes", AddProductType);
            app.MapPut("/producttypes/{id}", UpdateProductType);
            app.MapDelete("/producttypes/{id}", DeleteProductType);
        }

        private static IResult GetAllProductTypes(int? pageNumber, int? pageSize)
        {
            pageNumber ??= 1; // Default page number
            pageSize ??= 10;  // Default page size

            var paginatedProductTypes = productTypes
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToList();

            return Results.Ok(paginatedProductTypes);
        }

        private static IResult GetProductTypeById(int id)
        {
            var productType = productTypes.FirstOrDefault(pt => pt.Id == id);
            return productType is not null ? Results.Ok(productType) : Results.NotFound($"ProductType with ID {id} not found.");
        }

        private static IResult AddProductType(ProductType productType)
        {
            if (string.IsNullOrWhiteSpace(productType.Name))
                return Results.BadRequest("Name is required.");

            productType.Id = productTypes.Any() ? productTypes.Max(pt => pt.Id) + 1 : 1; // Auto-generate ID
            productTypes.Add(productType);

            return Results.Created($"/producttypes/{productType.Id}", productType);
        }

        private static IResult UpdateProductType(int id, ProductType updatedProductType)
        {
            var existingProductType = productTypes.FirstOrDefault(pt => pt.Id == id);
            if (existingProductType is null)
                return Results.NotFound($"ProductType with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(updatedProductType.Name))
                return Results.BadRequest("Name is required.");

            existingProductType.Name = updatedProductType.Name;

            return Results.Ok(existingProductType);
        }

        private static IResult DeleteProductType(int id)
        {
            var productType = productTypes.FirstOrDefault(pt => pt.Id == id);
            if (productType is null)
                return Results.NotFound($"ProductType with ID {id} not found.");

            productTypes.Remove(productType);
            return Results.Ok($"ProductType with ID {id} deleted.");
        }
    }
}
