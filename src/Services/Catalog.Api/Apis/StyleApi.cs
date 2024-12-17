using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api.Apis
{
    public static class StyleApi
    {
        private static readonly List<Style> styles = new();

        public static IEndpointRouteBuilder MapStyleApi(this IEndpointRouteBuilder app)
        {
            app.MapGet("/styles", GetAllStyles);
            app.MapGet("/styles/{id}", GetStyleById);
            app.MapPost("/styles", AddStyle);
            app.MapPut("/styles/{id}", UpdateStyle);
            app.MapDelete("/styles/{id}", DeleteStyle);

            return app;
        }

        public static async Task<Results<Ok<PaginatedItems<Style>>, BadRequest<string>>> GetAllStyles(
            [AsParameters] PaginationRequest paginationRequest)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = styles.Count;

            var itemsOnPage = styles
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            return TypedResults.Ok(new PaginatedItems<Style>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        public static async Task<Results<Ok<Style>, NotFound>> GetStyleById(
            int id)
        {
            var style = styles.FirstOrDefault(s => s.Id == id);
            return style is not null ? TypedResults.Ok(style) : TypedResults.NotFound();
        }

        public static async Task<Results<Created<Style>, BadRequest<string>>> AddStyle(
            Style style)
        {
            if (string.IsNullOrWhiteSpace(style.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            style.Id = styles.Any() ? styles.Max(s => s.Id) + 1 : 1;
            styles.Add(style);

            return TypedResults.Created($"/styles/{style.Id}", style);
        }

        public static async Task<Results<Ok<Style>, NotFound, BadRequest<string>>> UpdateStyle(
            int id, Style updatedStyle)
        {
            var existingStyle = styles.FirstOrDefault(s => s.Id == id);
            if (existingStyle is null)
            {
                return TypedResults.NotFound();
            }

            if (string.IsNullOrWhiteSpace(updatedStyle.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            existingStyle.Name = updatedStyle.Name;

            return TypedResults.Ok(existingStyle);
        }

        public static async Task<Results<Ok<string>, NotFound>> DeleteStyle(
            int id)
        {
            var style = styles.FirstOrDefault(s => s.Id == id);
            if (style is null)
            {
                return TypedResults.NotFound();
            }

            styles.Remove(style);

            return TypedResults.Ok($"Style with ID {id} deleted.");
        }
    }
}
