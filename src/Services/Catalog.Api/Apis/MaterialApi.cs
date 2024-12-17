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
            app.MapDelete("/materials", DeleteMaterial);

            return app;
        }

        private static readonly List<Material> materials = new();

        public static async Task<Results<Ok<PaginatedItems<Material>>, BadRequest<string>>> GetAllMaterials(
            [AsParameters] PaginationRequest paginationRequest)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = materials.Count;

            var itemsOnPage = materials
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            return TypedResults.Ok(new PaginatedItems<Material>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        public static async Task<Results<Ok<Material>, NotFound>> GetMaterialById(
            int id)
        {
            var material = materials.FirstOrDefault(m => m.Id == id);
            return material is not null ? TypedResults.Ok(material) : TypedResults.NotFound();
        }

        public static async Task<Results<Created<Material>, BadRequest<string>>> AddMaterial(
            Material material)
        {
            if (string.IsNullOrWhiteSpace(material.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            material.Id = materials.Any() ? materials.Max(m => m.Id) + 1 : 1;
            materials.Add(material);

            return TypedResults.Created($"/materials/{material.Id}", material);
        }

        public static async Task<Results<Ok<Material>, NotFound, BadRequest<string>>> UpdateMaterial(
            int id, Material updatedMaterial)
        {
            var existingMaterial = materials.FirstOrDefault(m => m.Id == id);
            if (existingMaterial == null)
            {
                return TypedResults.NotFound();
            }

            if (string.IsNullOrWhiteSpace(updatedMaterial.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            existingMaterial.Name = updatedMaterial.Name;

            return TypedResults.Ok(existingMaterial);
        }

        public static async Task<Results<Ok<string>, NotFound>> DeleteMaterial(
            int id)
        {
            var material = materials.FirstOrDefault(m => m.Id == id);
            if (material == null)
            {
                return TypedResults.NotFound();
            }

            materials.Remove(material);

            return TypedResults.Ok($"Material with ID {id} deleted.");
        }
    }
}
