using System.Net.Http.Json;
using WebComponent.Dtos;

namespace WebComponent.Services
{
    public class ShopService(HttpClient httpClient) : IShopService
    {
        private readonly string remoteServiceBaseUrl = "api/shop/";

        public Task<Shop?> GetShopById(int id)
        {
            var uri = $"{remoteServiceBaseUrl}shops/{id}";
            return httpClient.GetFromJsonAsync<Shop>(uri);
        }

        public async Task<IEnumerable<Shop>> GetAllShops()
        {
            var uri = $"{remoteServiceBaseUrl}shops";
            var result = await httpClient.GetFromJsonAsync<Shop[]>(uri);
            return result ?? Array.Empty<Shop>();
        }
        public async Task<IEnumerable<Shop>> GetShopsByCity(string city)
        {
            var uri = $"{remoteServiceBaseUrl}shops/city/{city}";
            var result = await httpClient.GetFromJsonAsync<Shop[]>(uri);
            return result ?? Array.Empty<Shop>();
        }
        public async Task<HttpResponseMessage> AddShop(Shop shop)
        {
            var uri = $"{remoteServiceBaseUrl}shops";
            return await httpClient.PostAsJsonAsync(uri, shop);
        }

        public async Task<HttpResponseMessage> UpdateShop(int id, Shop updatedShop)
        {
            var uri = $"{remoteServiceBaseUrl}shops/{id}";
            return await httpClient.PutAsJsonAsync(uri, updatedShop);
        }

        public async Task<HttpResponseMessage> DeleteShop(int id)
        {
            var uri = $"{remoteServiceBaseUrl}shops/{id}";
            return await httpClient.DeleteAsync(uri);
        }
        public async Task<IEnumerable<CatalogItem>> GetProductsByShopId(int shopId)
        {
            var uri = $"{remoteServiceBaseUrl}shops/{shopId}/products";
            var result = await httpClient.GetFromJsonAsync<CatalogItem[]>(uri);
            return result ?? Array.Empty<CatalogItem>();
        }
        public async Task<IEnumerable<CatalogItemType>> GetItemCategoriesByShopId(int shopId)
        {
            var uri = $"{remoteServiceBaseUrl}shops/{shopId}/categories";
            var result = await httpClient.GetFromJsonAsync<CatalogItemType[]>(uri);
            return result ?? Array.Empty<CatalogItemType>();
        }
    }
}
