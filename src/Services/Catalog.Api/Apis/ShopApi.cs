using Catalog.Infrastructure;

namespace Catalog.Api.Apis
{
    public class ShopApi
    {
        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/shops", GetShops);
            app.MapGet("/shops/{id}", GetShopById);
            app.MapPost("/shops", AddShop);
            app.MapPut("/shops/{id}", UpdateShop);
            app.MapDelete("/shops/{id}", DeleteShop);
            app.MapGet("/shops/products/{shopId}", GetShopProducts);
        }

        private static List<Shop> shopList = new List<Shop>();
        private static List<Product> productList = new List<Product>();

        private static IResult GetShops(int? pageNumber, int? pageSize)
        {
            pageNumber ??= 1;
            pageSize ??= 10;

            var paginatedShops = shopList
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToList();

            return Results.Ok(paginatedShops);
        }

        private static IResult GetShopById(int id)
        {
            var shop = shopList.FirstOrDefault(s => s.Id == id);
            return shop is not null ? Results.Ok(shop) : Results.NotFound($"Shop with ID {id} not found.");
        }

        private static IResult AddShop(Shop shop)
        {
            shop.Id = shopList.Any() ? shopList.Max(s => s.Id) + 1 : 1; // Auto-generate ID
            shopList.Add(shop);
            return Results.Created($"/shops/{shop.Id}", shop);
        }

        private static IResult UpdateShop(int id, Shop updatedShop)
        {
            var shop = shopList.FirstOrDefault(s => s.Id == id);
            if (shop == null)
            {
                return Results.NotFound($"Shop with ID {id} not found.");
            }

            shop.Name = updatedShop.Name;
            shop.City = updatedShop.City;
            shop.Address = updatedShop.Address;
            shop.ContactNumber = updatedShop.ContactNumber;
            shop.Rating = updatedShop.Rating;

            return Results.Ok(shop);
        }

        private static IResult DeleteShop(int id)
        {
            var shop = shopList.FirstOrDefault(s => s.Id == id);
            if (shop == null)
            {
                return Results.NotFound($"Shop with ID {id} not found.");
            }

            shopList.Remove(shop);
            return Results.Ok($"Shop with ID {id} deleted.");
        }

        private static IResult GetShopProducts(int shopId, int? pageNumber, int? pageSize)
        {
            pageNumber ??= 1;
            pageSize ??= 10;

            var shopProducts = productList.Where(p => p.ShopId == shopId);
            if (!shopProducts.Any())
            {
                return Results.NotFound($"No products found for shop ID {shopId}.");
            }

            var paginatedProducts = shopProducts
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToList();

            return Results.Ok(paginatedProducts);
        }
    }
}
