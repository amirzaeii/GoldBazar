﻿using System.Net.Http.Json;

namespace GoldBazar.Shared.Components.Services
{
    public class ShopService(HttpClient httpClient) 
    {
        private readonly string remoteServiceBaseUrl = "api/shop/";

        public Task<ShopDTO?> GetShopById(int id)
        {
            var uri = $"{remoteServiceBaseUrl}shops/{id}";
            return httpClient.GetFromJsonAsync<ShopDTO>(uri);
        }

        public async Task<IEnumerable<ShopDTO>> GetAllShops()
        {
            var uri = $"{remoteServiceBaseUrl}shops";
            var result = await httpClient.GetFromJsonAsync<ShopDTO[]>(uri);
            return result ?? Array.Empty<ShopDTO>();
        }
        public async Task<IEnumerable<ShopDTO>> GetShopsByCity(string city)
        {
            var uri = $"{remoteServiceBaseUrl}shops/city/{city}";
            var result = await httpClient.GetFromJsonAsync<ShopDTO[]>(uri);
            return result ?? Array.Empty<ShopDTO>();
        }
        public async Task<HttpResponseMessage> AddShop(ShopDTO shop)
        {
            var uri = $"{remoteServiceBaseUrl}shops";
            return await httpClient.PostAsJsonAsync(uri, shop);
        }

        public async Task<HttpResponseMessage> UpdateShop(int id, ShopDTO updatedShop)
        {
            var uri = $"{remoteServiceBaseUrl}shops/{id}";
            return await httpClient.PutAsJsonAsync(uri, updatedShop);
        }

        public async Task<HttpResponseMessage> DeleteShop(int id)
        {
            var uri = $"{remoteServiceBaseUrl}shops/{id}";
            return await httpClient.DeleteAsync(uri);
        }
        public async Task<IEnumerable<ItemDTO>> GetProductsByShopId(int shopId)
        {
            var uri = $"{remoteServiceBaseUrl}shops/{shopId}/products";
            var result = await httpClient.GetFromJsonAsync<ItemDTO[]>(uri);
            return result ?? Array.Empty<ItemDTO>();
        }
        public async Task<IEnumerable<ItemCategoryDTO>> GetItemCategoriesByShopId(int shopId)
        {
            var uri = $"{remoteServiceBaseUrl}shops/{shopId}/categories";
            var result = await httpClient.GetFromJsonAsync<ItemCategoryDTO[]>(uri);
            return result ?? Array.Empty<ItemCategoryDTO>();
        }
    }
}
