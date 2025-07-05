namespace Catalog.Api.Apis;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GoldBazar.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Catalog.Infrastructure.Models;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;

public static class PromotionsApi
{
 public static IEndpointRouteBuilder MapPromotionsApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/catalog").HasApiVersion(1.0);

        api.MapGet("/", GetPromotions)
            .WithName("GetPromotions")
            .WithSummary("Get all promotion sliders")
            .WithDescription("Retrieves a list of all promotional sliders.")
            .WithTags("Promotion");

        api.MapGet("/{id:int}", GetPromotionById)
            .WithName("GetPromotionById")
            .WithSummary("Get promotion by ID")
            .WithDescription("Retrieves a specific promotion by its unique identifier.")
            .WithTags("Promotion");

        api.MapPost("/", CreatePromotion)
            .WithName("CreatePromotion")
            .WithSummary("Create a new promotion")
            .WithDescription("Creates a new promotional slider.")
            .WithTags("Promotion");

        api.MapPut("/{id:int}", UpdatePromotion)
            .WithName("UpdatePromotion")
            .WithSummary("Update promotion")
            .WithDescription("Updates an existing promotional slider.")
            .WithTags("Promotion");

        api.MapDelete("/{id:int}", DeletePromotion)
            .WithName("DeletePromotion")
            .WithSummary("Delete promotion")
            .WithDescription("Deletes a promotion slider by ID.")
            .WithTags("Promotion");

        return app;
    }

public static async Task<Ok<List<PromotionSliderDTO>>> GetPromotions([AsParameters] CatalogServices services)
{
    var promotions = await services.Context.PromotionSlider
        .Select(p => new PromotionSliderDTO(
            p.Id, p.Title, p.Description, p.ImageUrl,
            p.Link, p.Priority, p.IsActive, p.CreatedAt, p.UpdatedAt))
        .ToListAsync();

    return TypedResults.Ok(promotions);
}

public static async Task<Results<Ok<PromotionSliderDTO>, NotFound>> GetPromotionById(int id, [AsParameters] CatalogServices services)
{
    var promotion = await services.Context.PromotionSlider
        .Where(p => p.Id == id)
        .Select(p => new PromotionSliderDTO(
            p.Id, p.Title, p.Description, p.ImageUrl,
            p.Link, p.Priority, p.IsActive, p.CreatedAt, p.UpdatedAt))
        .FirstOrDefaultAsync();

    return promotion is not null ? TypedResults.Ok(promotion) : TypedResults.NotFound();
}

public static async Task<Results<Created<PromotionSliderDTO>, BadRequest<string>>> CreatePromotion(
    [FromBody] PromotionSliderDTO dto,
    [AsParameters] CatalogServices services)
{
    if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.ImageUrl))
        return TypedResults.BadRequest("Title and ImageUrl are required.");

    var promotion = new PromotionSlider
    {
        Title = dto.Title,
        Description = dto.Description,
        ImageUrl = dto.ImageUrl,
        Link = dto.Link,
        Priority = dto.Priority,
        IsActive = dto.IsActive,
        CreatedAt = DateTime.UtcNow
    };

    services.Context.PromotionSlider.Add(promotion);
    await services.Context.SaveChangesAsync();

    var createdDto = new PromotionSliderDTO(
        promotion.Id, promotion.Title, promotion.Description, promotion.ImageUrl,
        promotion.Link, promotion.Priority, promotion.IsActive, promotion.CreatedAt, promotion.UpdatedAt);

    return TypedResults.Created($"/api/promotions/{promotion.Id}", createdDto);
}

public static async Task<Results<Ok<PromotionSliderDTO>, NotFound, BadRequest<string>>> UpdatePromotion(
    int id,
    [FromBody] PromotionSliderDTO dto,
    [AsParameters] CatalogServices services)
{
    var promotion = await services.Context.PromotionSlider.FindAsync(id);
    if (promotion is null)
        return TypedResults.NotFound();

    if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.ImageUrl))
        return TypedResults.BadRequest("Title and ImageUrl are required.");

    promotion.Title = dto.Title;
    promotion.Description = dto.Description;
    promotion.ImageUrl = dto.ImageUrl;
    promotion.Link = dto.Link;
    promotion.Priority = dto.Priority;
    promotion.IsActive = dto.IsActive;
    promotion.UpdatedAt = DateTime.UtcNow;

    await services.Context.SaveChangesAsync();

    return TypedResults.Ok(new PromotionSliderDTO(
        promotion.Id, promotion.Title, promotion.Description, promotion.ImageUrl,
        promotion.Link, promotion.Priority, promotion.IsActive, promotion.CreatedAt, promotion.UpdatedAt));
}

public static async Task<Results<Ok<string>, NotFound>> DeletePromotion(int id, [AsParameters] CatalogServices services)
{
    var promotion = await services.Context.PromotionSlider.FindAsync(id);
    if (promotion is null)
        return TypedResults.NotFound();

    services.Context.PromotionSlider.Remove(promotion);
    await services.Context.SaveChangesAsync();

    return TypedResults.Ok($"Promotion with ID {id} deleted.");
}
}

