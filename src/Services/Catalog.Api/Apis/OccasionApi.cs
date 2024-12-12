namespace Catalog.Api.Apis
{
    public static class OccasionApi
    {
        private static readonly List<Occassion> occasions = new();

        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/occasions", GetAllOccasions); // Supports paging
            app.MapGet("/occasions/{id}", GetOccasionById);
            app.MapPost("/occasions", AddOccasion);
            app.MapPut("/occasions/{id}", UpdateOccasion);
            app.MapDelete("/occasions/{id}", DeleteOccasion);
        }

        private static IResult GetAllOccasions(int? pageNumber, int? pageSize)
        {
            pageNumber ??= 1; // Default to first page
            pageSize ??= 10;  // Default to 10 items per page

            var paginatedOccasions = occasions
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToList();

            return Results.Ok(paginatedOccasions);
        }

        private static IResult GetOccasionById(int id)
        {
            var occasion = occasions.FirstOrDefault(o => o.Id == id);
            return occasion is not null ? Results.Ok(occasion) : Results.NotFound($"Occasion with ID {id} not found.");
        }

        private static IResult AddOccasion(Occassion occasion)
        {
            if (string.IsNullOrWhiteSpace(occasion.Name))
                return Results.BadRequest("Name is required.");

            occasion.Id = occasions.Any() ? occasions.Max(o => o.Id) + 1 : 1; // Auto-generate ID
            occasions.Add(occasion);

            return Results.Created($"/occasions/{occasion.Id}", occasion);
        }

        private static IResult UpdateOccasion(int id, Occassion updatedOccasion)
        {
            var existingOccasion = occasions.FirstOrDefault(o => o.Id == id);
            if (existingOccasion is null)
                return Results.NotFound($"Occasion with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(updatedOccasion.Name))
                return Results.BadRequest("Name is required.");

            existingOccasion.Name = updatedOccasion.Name;

            return Results.Ok(existingOccasion);
        }

        private static IResult DeleteOccasion(int id)
        {
            var occasion = occasions.FirstOrDefault(o => o.Id == id);
            if (occasion is null)
                return Results.NotFound($"Occasion with ID {id} not found.");

            occasions.Remove(occasion);
            return Results.Ok($"Occasion with ID {id} deleted.");
        }
    }
}
