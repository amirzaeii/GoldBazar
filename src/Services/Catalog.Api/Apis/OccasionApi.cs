using Catalog.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api.Apis
{
    public static class OccasionApi
    {
        public static IEndpointRouteBuilder MapOccasionApi(this IEndpointRouteBuilder app)
        {
            app.MapGet("/occasions", GetAllOccasions);
            app.MapGet("/occasions/{id}", GetOccasionById);
            app.MapPost("/occasions", AddOccasion);
            app.MapPut("/occasions/{id}", UpdateOccasion);
            app.MapDelete("/occasions/{id}", DeleteOccasion);

            return app;
        }

        public static async Task<Results<Ok<PaginatedItems<Occassion>>, BadRequest<string>>> GetAllOccasions(
            [AsParameters] PaginationRequest paginationRequest,
            [AsParameters] CatalogServices services)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = await services.Context.Occasions.LongCountAsync();

            var itemsOnPage = await services.Context.Occasions
                .OrderBy(o => o.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return TypedResults.Ok(new PaginatedItems<Occassion>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        public static async Task<Results<Ok<Occassion>, NotFound>> GetOccasionById(
            int id, [AsParameters] CatalogServices services)
        {
            var occasion = await services.Context.Occasions.FirstOrDefaultAsync(o => o.Id == id);
            return occasion is not null ? TypedResults.Ok(occasion) : TypedResults.NotFound();
        }

        public static async Task<Results<Created<Occassion>, BadRequest<string>>> AddOccasion(
            Occassion occasion, [AsParameters] CatalogServices services)
        {
            if (string.IsNullOrWhiteSpace(occasion.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            services.Context.Occasions.Add(occasion);
            await services.Context.SaveChangesAsync();

            return TypedResults.Created($"/occasions/{occasion.Id}", occasion);
        }

        public static async Task<Results<Ok<Occassion>, NotFound, BadRequest<string>>> UpdateOccasion(
            int id, Occassion updatedOccasion, [AsParameters] CatalogServices services)
        {
            var existingOccasion = await services.Context.Occasions.FirstOrDefaultAsync(o => o.Id == id);
            if (existingOccasion is null)
            {
                return TypedResults.NotFound();
            }

            if (string.IsNullOrWhiteSpace(updatedOccasion.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            existingOccasion.Name = updatedOccasion.Name;

            await services.Context.SaveChangesAsync();

            return TypedResults.Ok(existingOccasion);
        }

        public static async Task<Results<Ok<string>, NotFound>> DeleteOccasion(
            int id, [AsParameters] CatalogServices services)
        {
            var occasion = await services.Context.Occasions.FirstOrDefaultAsync(o => o.Id == id);
            if (occasion is null)
            {
                return TypedResults.NotFound();
            }

            services.Context.Occasions.Remove(occasion);
            await services.Context.SaveChangesAsync();

            return TypedResults.Ok($"Occasion with ID {id} deleted.");
        }
    }
}