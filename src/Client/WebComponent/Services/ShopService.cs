using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
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
    }
}
