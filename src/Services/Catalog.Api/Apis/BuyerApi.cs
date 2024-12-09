using Microsoft.AspNetCore.Http.HttpResults;
using System;

namespace GoldBazar.Api;

public static class BuyerApi
{
    public static IEndpointRouteBuilder MapBuyerApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/buyers").HasApiVersion(1.0);

        // CRUD Endpoints
        api.MapGet("/", GetAllBuyers); // Get all buyers
        api.MapGet("/{id:int}", GetBuyerById); // Get a specific buyer by ID
        api.MapPost("/", CreateBuyer); // Add a new buyer
        api.MapPut("/{id:int}", UpdateBuyer); // Update buyer details
        api.MapDelete("/{id:int}", DeleteBuyer); // Delete a buyer

        // Additional Features
        api.MapGet("/{id:int}/favorites", GetBuyerFavorites); // Fetch favorite goldsmiths or products
        api.MapPost("/{id:int}/favorites", AddToBuyerFavorites); // Add to favorites
        api.MapDelete("/{id:int}/favorites/{favoriteId:int}", RemoveFromBuyerFavorites); // Remove from favorites
        api.MapGet("/{id:int}/orders", GetBuyerOrders); // View order history

        return app;
    }

    // CRUD Operations
    public static async Task<Ok<List<Buyer>>> GetAllBuyers(BuyerContext context)
    {
        var buyers = await context.Buyers.ToListAsync();
        return TypedResults.Ok(buyers);
    }

    public static async Task<Results<Ok<Buyer>, NotFound>> GetBuyerById(BuyerContext context, int id)
    {
        var buyer = await context.Buyers.FindAsync(id);
        return buyer == null ? TypedResults.NotFound() : TypedResults.Ok(buyer);
    }

    public static async Task<Created> CreateBuyer(BuyerContext context, Buyer buyer)
    {
        context.Buyers.Add(buyer);
        await context.SaveChangesAsync();
        return TypedResults.Created($"/api/buyers/{buyer.Id}");
    }

    public static async Task<Results<NoContent, NotFound>> UpdateBuyer(BuyerContext context, int id, Buyer updatedBuyer)
    {
        var buyer = await context.Buyers.FindAsync(id);
        if (buyer == null)
        {
            return TypedResults.NotFound();
        }

        context.Entry(buyer).CurrentValues.SetValues(updatedBuyer);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> DeleteBuyer(BuyerContext context, int id)
    {
        var buyer = await context.Buyers.FindAsync(id);
        if (buyer == null)
        {
            return TypedResults.NotFound();
        }

        context.Buyers.Remove(buyer);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    // Favorites Management
    public static async Task<Ok<List<FavoriteItem>>> GetBuyerFavorites(BuyerContext context, int id)
    {
        var favorites = await context.Favorites
            .Where(f => f.BuyerId == id)
            .ToListAsync();
        return TypedResults.Ok(favorites);
    }

    public static async Task<Created> AddToBuyerFavorites(BuyerContext context, int id, FavoriteItem favoriteItem)
    {
        favoriteItem.BuyerId = id;
        context.Favorites.Add(favoriteItem);
        await context.SaveChangesAsync();
        return TypedResults.Created($"/api/buyers/{id}/favorites");
    }

    public static async Task<Results<NoContent, NotFound>> RemoveFromBuyerFavorites(BuyerContext context, int id, int favoriteId)
    {
        var favorite = await context.Favorites.FindAsync(favoriteId);
        if (favorite == null || favorite.BuyerId != id)
        {
            return TypedResults.NotFound();
        }

        context.Favorites.Remove(favorite);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    // Order History
    public static async Task<Ok<List<Order>>> GetBuyerOrders(BuyerContext context, int id)
    {
        var orders = await context.Orders
            .Where(o => o.BuyerId == id)
            .ToListAsync();
        return TypedResults.Ok(orders);
    }
}

/* The corresponding classes
public class Buyer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }
    public string Address { get; set; }
}
public class FavoriteItem
{
    public int Id { get; set; }
    public int BuyerId { get; set; }
    public int ItemId { get; set; } // ID of the goldsmith or product
    public string ItemType { get; set; } // "Goldsmith" or "Product"
    public DateTime AddedDate { get; set; }
}

*/