using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Apis
{
    public static class MaterialApi
    {
        public static IEndpointRouteBuilder MapMaterialApi(this IEndpointRouteBuilder app)
        {
            app.MapGet("/materials", GetAllMaterials);
            app.MapGet("/materials/{id}", GetMaterialById);
            app.MapPost("/materials", AddMaterial);
            app.MapPut("/materials/{id}", UpdateMaterial);
            app.MapDelete("/materials/{id}", DeleteMaterial);

            return app;
        }

        public static async Task<Results<Ok<PaginatedItems<Material>>, BadRequest<string>>> GetAllMaterials(
            [AsParameters] PaginationRequest paginationRequest,
            [AsParameters] CatalogServices services)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = await services.Context.Materials.LongCountAsync();

            var itemsOnPage = await services.Context.Materials
                .OrderBy(m => m.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return TypedResults.Ok(new PaginatedItems<Material>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        public static async Task<Results<Ok<Material>, NotFound>> GetMaterialById(
            int id, [AsParameters] CatalogServices services)
        {
            var material = await services.Context.Materials.FirstOrDefaultAsync(m => m.Id == id);
            return material is not null ? TypedResults.Ok(material) : TypedResults.NotFound();
        }

        public static async Task<Results<Created<Material>, BadRequest<string>>> AddMaterial(
            Material material, [AsParameters] CatalogServices services)
        {
            if (string.IsNullOrWhiteSpace(material.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            services.Context.Materials.Add(material);
            await services.Context.SaveChangesAsync();

            return TypedResults.Created($"/materials/{material.Id}", material);
        }

        public static async Task<Results<Ok<Material>, NotFound, BadRequest<string>>> UpdateMaterial(
            int id, Material updatedMaterial, [AsParameters] CatalogServices services)
        {
            var existingMaterial = await services.Context.Materials.FirstOrDefaultAsync(m => m.Id == id);

            if (existingMaterial == null)
            {
                return TypedResults.NotFound();
            }

            if (string.IsNullOrWhiteSpace(updatedMaterial.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            existingMaterial.Name = updatedMaterial.Name;

            await services.Context.SaveChangesAsync();

            return TypedResults.Ok(existingMaterial);
        }

        public static async Task<Results<Ok<string>, NotFound>> DeleteMaterial(
            int id, [AsParameters] CatalogServices services)
        {
            var material = await services.Context.Materials.FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return TypedResults.NotFound();
            }

            services.Context.Materials.Remove(material);
            await services.Context.SaveChangesAsync();

            return TypedResults.Ok($"Material with ID {id} deleted.");
        }
    }
}