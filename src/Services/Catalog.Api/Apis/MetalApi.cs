namespace Catalog.Api.Apis
{
    public static class MetalApi
    {
        private static readonly List<Metal> metals = new();

        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/metals", GetAllMetals); // Supports paging
            app.MapGet("/metals/{id}", GetMetalById);
            app.MapPost("/metals", AddMetal);
            app.MapPut("/metals/{id}", UpdateMetal);
            app.MapDelete("/metals/{id}", DeleteMetal);
        }

        private static IResult GetAllMetals(int? pageNumber, int? pageSize)
        {
            pageNumber ??= 1; // Default page number
            pageSize ??= 10;  // Default page size

            var paginatedMetals = metals
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToList();

            return Results.Ok(paginatedMetals);
        }

        private static IResult GetMetalById(int id)
        {
            var metal = metals.FirstOrDefault(m => m.Id == id);
            return metal is not null ? Results.Ok(metal) : Results.NotFound($"Metal with ID {id} not found.");
        }

        private static IResult AddMetal(Metal metal)
        {
            if (string.IsNullOrWhiteSpace(metal.Name))
                return Results.BadRequest("Name is required.");
            if (metal.Manufacture == 0)
                return Results.BadRequest("Manufacture is required.");
            if (metal.Karat < 18 || metal.Karat > 24)
                return Results.BadRequest("KT must be between 18 and 24.");

            metal.Id = metals.Any() ? metals.Max(m => m.Id) + 1 : 1; // Auto-generate ID

            metals.Add(metal);
            return Results.Created($"/metals/{metal.Id}", metal);
        }

        private static IResult UpdateMetal(int id, Metal updatedMetal)
        {
            var existingMetal = metals.FirstOrDefault(m => m.Id == id);
            if (existingMetal is null)
                return Results.NotFound($"Metal with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(updatedMetal.Name))
                return Results.BadRequest("Name is required.");
            if (updatedMetal.Manufacture == 0)
                return Results.BadRequest("Manufacture is required.");
            if (updatedMetal.Karat < 18 || updatedMetal.Karat > 24)
                return Results.BadRequest("KT must be between 18 and 24.");

            existingMetal.Name = updatedMetal.Name;
            existingMetal.Manufacture = updatedMetal.Manufacture;
            existingMetal.Karat = updatedMetal.Karat;

            return Results.Ok(existingMetal);
        }

        private static IResult DeleteMetal(int id)
        {
            var metal = metals.FirstOrDefault(m => m.Id == id);
            if (metal is null)
                return Results.NotFound($"Metal with ID {id} not found.");

            metals.Remove(metal);
            return Results.Ok($"Metal with ID {id} deleted.");
        }
    }
}
