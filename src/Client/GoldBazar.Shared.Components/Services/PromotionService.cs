using System.Net.Http.Json;

namespace GoldBazar.Shared.Components.Services;

public class PromotionService(HttpClient httpClient)
{
    private readonly string baseUrl = "api/catalog";

    public async Task<IEnumerable<PromotionSliderDTO>> GetPromotions()
    {
        var uri = $"{baseUrl}/";
        var result = await httpClient.GetFromJsonAsync<IEnumerable<PromotionSliderDTO>>(uri);
        return result ?? Enumerable.Empty<PromotionSliderDTO>();
    }

    public async Task<PromotionSliderDTO?> GetPromotionById(int id)
    {
        var uri = $"{baseUrl}/{id}";
        return await httpClient.GetFromJsonAsync<PromotionSliderDTO>(uri);
    }

    public async Task<PromotionSliderDTO?> AddPromotion(PromotionSliderDTO dto)
    {
        var uri = $"{baseUrl}/";
        var response = await httpClient.PostAsJsonAsync(uri, dto);
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<PromotionSliderDTO>();
    }

    public async Task<PromotionSliderDTO?> UpdatePromotion(PromotionSliderDTO dto)
    {
        var uri = $"{baseUrl}/{dto.Id}";
        var response = await httpClient.PutAsJsonAsync(uri, dto);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to update promotion: {response.StatusCode}");
            return null;
        }

        return await response.Content.ReadFromJsonAsync<PromotionSliderDTO>();
    }

    public async Task<bool> DeletePromotion(int id)
    {
        var uri = $"{baseUrl}/{id}";
        var response = await httpClient.DeleteAsync(uri);
        return response.IsSuccessStatusCode;
    }
}

