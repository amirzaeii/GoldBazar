
using System.Net.Http.Json;
using System.Text.Json;

namespace GoldBazar.Shared.Components.Services;

public class RegionService(HttpClient httpClient)
{
    private readonly HttpClient _http = httpClient;
    private const string BaseUrl = "api/catalog/";
    private const string GovBase = "api/catalog/governorates";

    // GOVERNORATES
    public async Task<GovernateDTO[]> GetGovernorates()
        => await _http.GetFromJsonAsync<GovernateDTO[]>(GovBase)
           ?? Array.Empty<GovernateDTO>();

    public async Task<GovernateDTO> GetGovernorateById(int id)
    {
        var dto = await _http.GetFromJsonAsync<GovernateDTO>($"{GovBase}/{id}");
        if (dto is null) throw new InvalidOperationException($"No governorate with ID {id}");
        return dto;
    }

    public async Task<GovernateDTO?> AddGovernorate(GovernateDTO newGov)
    {
        var resp = await _http.PostAsJsonAsync(GovBase, newGov);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<GovernateDTO>();
    }

    public async Task<GovernateDTO?> UpdateGovernorate(GovernateDTO gov)
    {
        var resp = await _http.PutAsJsonAsync($"{GovBase}/{gov.Id}", gov);
        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<GovernateDTO>();
    }

    public async Task<bool> DeleteGovernorate(int id)
        => (await _http.DeleteAsync($"{GovBase}/{id}")).IsSuccessStatusCode;

    // Cities

    public async Task<CityDTO[]> GetCities()
    {
        var uri = $"{BaseUrl}cities";
        return await _http.GetFromJsonAsync<CityDTO[]>(uri)
               ?? Array.Empty<CityDTO>();
    }

    public async Task<CityDTO> GetCityById(int id)
    {
        var uri = $"{BaseUrl}cities/{id}";
        return await _http.GetFromJsonAsync<CityDTO>(uri)
               ?? throw new InvalidOperationException($"No city with ID {id}");
    }

    public async Task<CityDTO[]> GetCitiesByGovernorate(int governorateId)
    {
        var uri = $"{BaseUrl}cities/governorate/{governorateId}";
        return await _http.GetFromJsonAsync<CityDTO[]>(uri)
               ?? Array.Empty<CityDTO>();
    }

    public async Task<CityDTO> AddCity(CityDTO newCity)
    {
        var uri = $"{BaseUrl}cities";
        Console.WriteLine($"POST {uri} → {JsonSerializer.Serialize(newCity)}");
        var resp = await _http.PostAsJsonAsync(uri, newCity);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<CityDTO>()!;
    }

    public async Task<CityDTO> UpdateCity(CityDTO city)
    {
        var uri = $"{BaseUrl}cities/{city.Id}";
        Console.WriteLine($"PUT {uri} → {JsonSerializer.Serialize(city)}");
        var resp = await _http.PutAsJsonAsync(uri, city);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<CityDTO>()!;
    }

    public async Task<bool> DeleteCity(int id)
    {
        var uri = $"{BaseUrl}cities/{id}";
        Console.WriteLine($"DELETE {uri}");
        var resp = await _http.DeleteAsync(uri);
        return resp.IsSuccessStatusCode;
    }
}
