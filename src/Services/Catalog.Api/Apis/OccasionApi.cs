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
        private static readonly List<Occassion> occasions = new();

        public static async Task<Results<Ok<PaginatedItems<Occassion>>, BadRequest<string>>> GetAllOccasions(
            [AsParameters] PaginationRequest paginationRequest)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = occasions.Count;

            var itemsOnPage = occasions
                .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToList();

            return TypedResults.Ok(new PaginatedItems<Occassion>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        public static async Task<Results<Ok<Occassion>, NotFound>> GetOccasionById(
            int id)
        {
            var occasion = occasions.FirstOrDefault(o => o.Id == id);
            return occasion is not null ? TypedResults.Ok(occasion) : TypedResults.NotFound();
        }

        public static async Task<Results<Created<Occassion>, BadRequest<string>>> AddOccasion(
            Occassion occasion)
        {
            if (string.IsNullOrWhiteSpace(occasion.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            occasion.Id = occasions.Any() ? occasions.Max(o => o.Id) + 1 : 1;
            occasions.Add(occasion);

            return TypedResults.Created($"/occasions/{occasion.Id}", occasion);
        }

        public static async Task<Results<Ok<Occassion>, NotFound, BadRequest<string>>> UpdateOccasion(
            int id, Occassion updatedOccasion)
        {
            var existingOccasion = occasions.FirstOrDefault(o => o.Id == id);
            if (existingOccasion is null)
            {
                return TypedResults.NotFound();
            }

            if (string.IsNullOrWhiteSpace(updatedOccasion.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            existingOccasion.Name = updatedOccasion.Name;

            return TypedResults.Ok(existingOccasion);
        }

        public static async Task<Results<Ok<string>, NotFound>> DeleteOccasion(
            int id)
        {
            var occasion = occasions.FirstOrDefault(o => o.Id == id);
            if (occasion is null)
            {
                return TypedResults.NotFound();
            }

            occasions.Remove(occasion);
            return TypedResults.Ok($"Occasion with ID {id} deleted.");
        }
    }
}
