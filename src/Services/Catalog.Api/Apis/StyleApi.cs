using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api.Apis
{
    public static class StyleApi
    {
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
            [AsParameters] PaginationRequest paginationRequest,
            [AsParameters] CatalogServices services)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = await services.Context.Styles.LongCountAsync();

            var itemsOnPage = await services.Context.Styles
                .OrderBy(s => s.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return TypedResults.Ok(new PaginatedItems<Style>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        public static async Task<Results<Ok<Style>, NotFound>> GetStyleById(
            int id, [AsParameters] CatalogServices services)
        {
            var style = await services.Context.Styles.FirstOrDefaultAsync(s => s.Id == id);
            return style is not null ? TypedResults.Ok(style) : TypedResults.NotFound();
        }

        public static async Task<Results<Created<Style>, BadRequest<string>>> AddStyle(
            Style style, [AsParameters] CatalogServices services)
        {
            if (string.IsNullOrWhiteSpace(style.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            services.Context.Styles.Add(style);
            await services.Context.SaveChangesAsync();

            return TypedResults.Created($"/styles/{style.Id}", style);
        }

        public static async Task<Results<Ok<Style>, NotFound, BadRequest<string>>> UpdateStyle(
            int id, Style updatedStyle, [AsParameters] CatalogServices services)
        {
            var existingStyle = await services.Context.Styles.FirstOrDefaultAsync(s => s.Id == id);
            if (existingStyle is null)
            {
                return TypedResults.NotFound();
            }

            if (string.IsNullOrWhiteSpace(updatedStyle.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            existingStyle.Name = updatedStyle.Name;

            await services.Context.SaveChangesAsync();

            return TypedResults.Ok(existingStyle);
        }

        public static async Task<Results<Ok<string>, NotFound>> DeleteStyle(
            int id, [AsParameters] CatalogServices services)
        {
            var style = await services.Context.Styles.FirstOrDefaultAsync(s => s.Id == id);
            if (style is null)
            {
                return TypedResults.NotFound();
            }

            services.Context.Styles.Remove(style);
            await services.Context.SaveChangesAsync();

            return TypedResults.Ok($"Style with ID {id} deleted.");
        }
    }
}
