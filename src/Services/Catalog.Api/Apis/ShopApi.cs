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

        public static async Task<Results<Ok<PaginatedItems<Shop>>, NotFound>> GetShops(
            [AsParameters] PaginationRequest paginationRequest,
            [AsParameters] CatalogServices services)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var totalItems = await services.Context.Shops.LongCountAsync();

            var itemsOnPage = await services.Context.Shops
                .OrderBy(s => s.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            if (!itemsOnPage.Any())
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new PaginatedItems<Shop>(pageIndex, pageSize, totalItems, itemsOnPage));
        }

        public static async Task<Results<Ok<Shop>, NotFound>> GetShopById(
            int id, [AsParameters] CatalogServices services)
        {
            var shop = await services.Context.Shops.FirstOrDefaultAsync(s => s.Id == id);
            return shop is not null ? TypedResults.Ok(shop) : TypedResults.NotFound();
        }

        public static async Task<Results<Created<Shop>, BadRequest<string>>> AddShop(
            Shop shop, [AsParameters] CatalogServices services)
        {
            if (string.IsNullOrWhiteSpace(shop.Name))
            {
                return TypedResults.BadRequest("Shop name is required.");
            }

            services.Context.Shops.Add(shop);
            await services.Context.SaveChangesAsync();

            return TypedResults.Created($"/shops/{shop.Id}", shop);
        }

        public static async Task<Results<Ok<Shop>, NotFound, BadRequest<string>>> UpdateShop(
            int id, Shop updatedShop, [AsParameters] CatalogServices services)
        {
            var shop = await services.Context.Shops.FirstOrDefaultAsync(s => s.Id == id);
            if (shop is null)
            {
                return TypedResults.NotFound();
            }

            if (string.IsNullOrWhiteSpace(updatedShop.Name))
            {
                return TypedResults.BadRequest("Shop name is required.");
            }

            shop.Name = updatedShop.Name;
            shop.City = updatedShop.City;
            shop.Address = updatedShop.Address;
            shop.ContactNumber = updatedShop.ContactNumber;
            shop.Owner = updatedShop.Owner;

            await services.Context.SaveChangesAsync();

            return TypedResults.Ok(shop);
        }

        public static async Task<Results<Ok<string>, NotFound>> DeleteShop(
            int id, [AsParameters] CatalogServices services)
        {
            var shop = await services.Context.Shops.FirstOrDefaultAsync(s => s.Id == id);
            if (shop is null)
            {
                return TypedResults.NotFound();
            }

            services.Context.Shops.Remove(shop);
            await services.Context.SaveChangesAsync();

            return TypedResults.Ok($"Shop with ID {id} deleted.");
        }

        public static async Task<Results<Ok<PaginatedItems<Product>>, NotFound>> GetShopProducts(
            int shopId, [AsParameters] PaginationRequest paginationRequest,
            [AsParameters] CatalogServices services)
        {
            var pageSize = paginationRequest.PageSize;
            var pageIndex = paginationRequest.PageIndex;

            var shopProducts = await services.Context.Products
                .Where(p => p.ShopId == shopId)
                .ToListAsync();

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
