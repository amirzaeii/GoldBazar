using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Apis
{
    public static class MaterialApi
    {
        private static readonly List<Material> materials = new();

        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/materials", GetAllMaterials); // Paging added
            app.MapGet("/materials/{id}", GetMaterialById);
            app.MapPost("/materials", AddMaterial);
            app.MapPut("/materials/{id}", UpdateMaterial);
            app.MapDelete("/materials/{id}", DeleteMaterial);
        }

        private static IResult GetAllMaterials(int? pageNumber, int? pageSize)
        {
            pageNumber ??= 1; // Default to first page
            pageSize ??= 10;  // Default to 10 items per page

            var paginatedMaterials = materials
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToList();

            return Results.Ok(paginatedMaterials);
        }

        private static IResult GetMaterialById(int id)
        {
            var material = materials.FirstOrDefault(m => m.Id == id);
            return material is not null ? Results.Ok(material) : Results.NotFound($"Material with ID {id} not found.");
        }

        private static IResult AddMaterial(MaterialDto materialDto)
        {
            if (string.IsNullOrWhiteSpace(materialDto.Name))
                return Results.BadRequest("Name is required.");

            var material = new Material
            {
                Id = materials.Any() ? materials.Max(m => m.Id) + 1 : 1, // Auto-generate ID
                Name = materialDto.Name
            };

            materials.Add(material);
            return Results.Created($"/materials/{material.Id}", material);
        }

        private static IResult UpdateMaterial(int id, MaterialDto updatedMaterialDto)
        {
            var existingMaterial = materials.FirstOrDefault(m => m.Id == id);
            if (existingMaterial == null)
                return Results.NotFound($"Material with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(updatedMaterialDto.Name))
                return Results.BadRequest("Name is required.");

            // Create a new instance for immutability
            var updatedMaterial = new Material
            {
                Id = existingMaterial.Id, // Preserve ID
                Name = updatedMaterialDto.Name
            };

            // Replace old material with the updated one
            materials.Remove(existingMaterial);
            materials.Add(updatedMaterial);

            return Results.Ok(updatedMaterial);
        }

        private static IResult DeleteMaterial(int id)
        {
            var material = materials.FirstOrDefault(m => m.Id == id);
            if (material == null)
                return Results.NotFound($"Material with ID {id} not found.");

            materials.Remove(material);
            return Results.Ok($"Material with ID {id} deleted.");
        }
    }
}
