using System.Net.Http.Json;
namespace GoldBazar.Shared.Components.Services;

public class ShopService(HttpClient httpClient)
{
    private readonly string remoteServiceBaseUrl = "api/";
    public async Task<ShopDTO> GetShopById(int id)
    {
        var uri = $"{remoteServiceBaseUrl}shops/{id}";
        return await httpClient.GetFromJsonAsync<ShopDTO>(uri)
               ?? throw new InvalidOperationException($"No shop with ID {id}");
    }

    public async Task<ShopDTO[]> GetAllShops()
    {
        var uri = $"{remoteServiceBaseUrl}shops";
        return await httpClient.GetFromJsonAsync<ShopDTO[]>(uri)
               ?? Array.Empty<ShopDTO>();
    }

    public async Task<ShopDTO[]> GetShopsByCity(string city)
    {
        var uri = $"{remoteServiceBaseUrl}shops/city/{city}";
        return await httpClient.GetFromJsonAsync<ShopDTO[]>(uri)
               ?? Array.Empty<ShopDTO>();
    }

    public async Task<ShopDTO> AddShop(ShopDTO shop)
    {
        var r = await httpClient.PostAsJsonAsync($"{remoteServiceBaseUrl}shops", shop);
        r.EnsureSuccessStatusCode();
        return (await r.Content.ReadFromJsonAsync<ShopDTO>())!;
    }

    public async Task<ShopDTO> UpdateShop(int id, ShopDTO shop)
    {
        var r = await httpClient.PutAsJsonAsync($"api/shops/{id}", shop);
        r.EnsureSuccessStatusCode();
        return (await r.Content.ReadFromJsonAsync<ShopDTO>())!;
    }

    public async Task<bool> DeleteShop(int id)
    {
        var r = await httpClient.DeleteAsync($"api/shops/{id}");
        return r.IsSuccessStatusCode;
    }

    public async Task<ItemDTO[]> GetItemsByShopId(int shopId)
    {
        var uri = $"{remoteServiceBaseUrl}shops/{shopId}/items";
        return await httpClient.GetFromJsonAsync<ItemDTO[]>(uri)
               ?? Array.Empty<ItemDTO>();
    }

    public async Task<ItemCategoryDTO[]> GetItemCategoriesByShopId(int shopId)
    {
        var uri = $"{remoteServiceBaseUrl}shops/{shopId}/categories";
        return await httpClient.GetFromJsonAsync<ItemCategoryDTO[]>(uri)
               ?? Array.Empty<ItemCategoryDTO>();
    }
}