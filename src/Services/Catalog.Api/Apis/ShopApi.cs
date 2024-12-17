using Catalog.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api.Apis
{
    //The class is now declared as static, which is required for extension methods.
    public static class ShopApi
    {
        public static IEndpointRouteBuilder MapShopApi(this IEndpointRouteBuilder app)
        {
            app.MapGet("/shops", GetShops);
            app.MapGet("/shops/{id}", GetShopById);
            app.MapPost("/shops", AddShop);
            app.MapPut("/shops/{id}", UpdateShop);
            app.MapDelete("/shops/{id}", DeleteShop);
            app.MapGet("/shops/products/{shopId}", GetShopProducts);

            return app;
        }

        private static readonly List<Shop> shopList = new();
        private static readonly List<Product> productList = new();

        public static async Task<Results<Ok<PaginatedItems<Shop>>, NotFound>> GetShops(
            [AsParameters] PaginationRequest paginationRequest)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = shopList.Count;

            var itemsOnPage = shopList
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            if (!itemsOnPage.Any())
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new PaginatedItems<Shop>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        public static async Task<Results<Ok<Shop>, NotFound>> GetShopById(
            int id)
        {
            var shop = shopList.FirstOrDefault(s => s.Id == id);
            return shop is not null ? TypedResults.Ok(shop) : TypedResults.NotFound();
        }

        public static async Task<Results<Created<Shop>, BadRequest<string>>> AddShop(
            Shop shop)
        {
            if (string.IsNullOrWhiteSpace(shop.Name))
            {
                return TypedResults.BadRequest("Shop name is required.");
            }

            shop.Id = shopList.Any() ? shopList.Max(s => s.Id) + 1 : 1;
            shopList.Add(shop);

            return TypedResults.Created($"/shops/{shop.Id}", shop);
        }

        public static async Task<Results<Ok<Shop>, NotFound, BadRequest<string>>> UpdateShop(
            int id, Shop updatedShop)
        {
            var shop = shopList.FirstOrDefault(s => s.Id == id);
            if (shop is null)
            {
                return TypedResults.NotFound();
            }

            if (string.IsNullOrWhiteSpace(updatedShop.Name))
            {
                return TypedResults.BadRequest("Name is required.");
            }

            shop.Name = updatedShop.Name;
            shop.City = updatedShop.City;
            shop.Address = updatedShop.Address;
            shop.ContactNumber = updatedShop.ContactNumber;
            shop.Owner = updatedShop.Owner;

            return TypedResults.Ok(shop);
        }

        public static async Task<Results<Ok<string>, NotFound>> DeleteShop(
            int id)
        {
            var shop = shopList.FirstOrDefault(s => s.Id == id);
            if (shop is null)
            {
                return TypedResults.NotFound();
            }

            shopList.Remove(shop);

            return TypedResults.Ok($"Shop with ID {id} deleted.");
        }

        public static async Task<Results<Ok<PaginatedItems<Product>>, NotFound>> GetShopProducts(
            int shopId, [AsParameters] PaginationRequest paginationRequest)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var shopProducts = productList.Where(p => p.ShopId == shopId).ToList();

            if (!shopProducts.Any())
            {
                return TypedResults.NotFound();
            }

            var totalItems = shopProducts.Count;

            var itemsOnPage = shopProducts
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToList();

            return TypedResults.Ok(new PaginatedItems<Product>(pageIndex, pageSize, totalItems, itemsOnPage));
        }
    }
}
