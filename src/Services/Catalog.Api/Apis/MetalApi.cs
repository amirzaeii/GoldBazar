using GoldBazar.Shared.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api.Apis
{
    public static class MetalApi
    {
        public static IEndpointRouteBuilder MapMetalApi(this IEndpointRouteBuilder app)
        {
            app.MapGet("/metals", GetAllMetals);
            app.MapGet("/metals/{id}", GetMetalById);
            app.MapPost("/metals", AddMetal);
            app.MapPut("/metals/{id}", UpdateMetal);
            app.MapDelete("/metals/{id}", DeleteMetal);

            return app;
        }

        private static readonly List<Metal> metals = new();

        public static async Task<Results<Ok<PaginatedItems<Metal>>, BadRequest<string>>> GetAllMetals(
            [AsParameters] PaginationRequest paginationRequest)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = metals.Count;

            var itemsOnPage = metals
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            return TypedResults.Ok(new PaginatedItems<Metal>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        public static async Task<Results<Ok<Metal>, NotFound>> GetMetalById(
            int id)
        {
            var metal = metals.FirstOrDefault(m => m.Id == id);
            return metal is not null ? TypedResults.Ok(metal) : TypedResults.NotFound();
        }

        public static async Task<Results<Created<Metal>, BadRequest<string>>> AddMetal(
            Metal metal)
        {
            if (!Enum.IsDefined(typeof(ManufactureEnum), metal.Manufacture))
            {
                return TypedResults.BadRequest("Invalid Manufacture value.");
            }

            if (metal.Karat < 18 || metal.Karat > 24)
            {
                return TypedResults.BadRequest("Karat must be between 18 and 24.");
            }

            metal.Id = metals.Any() ? metals.Max(m => m.Id) + 1 : 1;
            metals.Add(metal);

            return TypedResults.Created($"/metals/{metal.Id}", metal);
        }

        public static async Task<Results<Ok<Metal>, NotFound, BadRequest<string>>> UpdateMetal(
            int id, Metal updatedMetal)
        {
            var existingMetal = metals.FirstOrDefault(m => m.Id == id);
            if (existingMetal is null)
            {
                return TypedResults.NotFound();
            }

            if (!Enum.IsDefined(typeof(ManufactureEnum), updatedMetal.Manufacture))
            {
                return TypedResults.BadRequest("Invalid Manufacture value.");
            }

            if (updatedMetal.Karat < 18 || updatedMetal.Karat > 24)
            {
                return TypedResults.BadRequest("Karat must be between 18 and 24.");
            }

            existingMetal.Name = updatedMetal.Name;
            existingMetal.Manufacture = updatedMetal.Manufacture;
            existingMetal.Karat = updatedMetal.Karat;

            return TypedResults.Ok(existingMetal);
        }

        public static async Task<Results<Ok<string>, NotFound>> DeleteMetal(
            int id)
        {
            var metal = metals.FirstOrDefault(m => m.Id == id);
            if (metal is null)
            {
                return TypedResults.NotFound();
            }

            metals.Remove(metal);
            return TypedResults.Ok($"Metal with ID {id} deleted.");
        }
    }
    }
