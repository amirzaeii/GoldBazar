namespace Catalog.Api.Apis
{
    public static class StyleApi
    {
        // Static list to simulate a database
        private static readonly List<Style> styles = new();

        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/styles", GetStyles); // Paging added
            app.MapGet("/styles/{id}", GetStyleById);
            app.MapPost("/styles", AddStyle);
            app.MapPut("/styles/{id}", UpdateStyle);
            app.MapDelete("/styles/{id}", DeleteStyle);
        }

        private static IResult GetStyles(int? pageNumber, int? pageSize)
        {
            pageNumber ??= 1; // Default page number
            pageSize ??= 10;  // Default page size

            var paginatedStyles = styles
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .Select(s => new StyleDto(s))
                .ToList();

            return Results.Ok(paginatedStyles);
        }

        private static IResult GetStyleById(int id)
        {
            var style = styles.FirstOrDefault(s => s.Id == id);
            if (style == null)
                return Results.NotFound($"Style with ID {id} not found.");

            return Results.Ok(new StyleDto(style));
        }

        private static IResult AddStyle(StyleDto styleDto)
        {
            if (string.IsNullOrWhiteSpace(styleDto.Name))
                return Results.BadRequest("Name is required.");

            var style = new Style
            {
                Id = styles.Any() ? styles.Max(s => s.Id) + 1 : 1, // Auto-generate ID
                Name = styleDto.Name
            };

            styles.Add(style);
            return Results.Created($"/styles/{style.Id}", new StyleDto(style));
        }

        private static IResult UpdateStyle(int id, StyleDto updatedStyleDto)
        {
            var style = styles.FirstOrDefault(s => s.Id == id);
            if (style == null)
                return Results.NotFound($"Style with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(updatedStyleDto.Name))
                return Results.BadRequest("Name is required.");

            // Create a new instance to maintain immutability
            var updatedStyle = new Style
            {
                Id = style.Id, // Preserve ID
                Name = updatedStyleDto.Name
            };

            // Replace old style with the updated one
            styles.Remove(style);
            styles.Add(updatedStyle);

            return Results.Ok(new StyleDto(updatedStyle));
        }

        private static IResult DeleteStyle(int id)
        {
            var style = styles.FirstOrDefault(s => s.Id == id);
            if (style == null)
                return Results.NotFound($"Style with ID {id} not found.");

            styles.Remove(style);
            return Results.Ok($"Style with ID {id} deleted.");
        }
    }
}
