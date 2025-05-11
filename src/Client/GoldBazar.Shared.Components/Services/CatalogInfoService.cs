using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GoldBazar.Shared.Components.Services;

public class CatalogInfoService(HttpClient httpClient)
{
    private readonly string remoteServiceBaseUrl = "api/catalog/";

    //// Materials
    //public async Task<MaterialDTO[]> GetMaterials()
    //{
    //    var uri = $"{remoteServiceBaseUrl}materials";
    //    var result = await httpClient.GetFromJsonAsync<MaterialDTO[]>(uri);
    //    return result ?? Array.Empty<MaterialDTO>();
    //}

    //public async Task<MaterialDTO> GetMaterialById(int id)
    //{
    //    var uri = $"{remoteServiceBaseUrl}materials/{id}";
    //    var result = await httpClient.GetFromJsonAsync<MaterialDTO>(uri);
    //    return result!;
    //}

    //public async Task<bool> AddMaterial(MaterialDTO material)
    //{
    //    var uri = $"{remoteServiceBaseUrl}materials";
    //    try
    //    {
    //        var response = await httpClient.PostAsJsonAsync(uri, material);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error creating material: {ex.Message}");
    //        return false;
    //    }
    //}

    //public async Task<bool> UpdateMaterial(MaterialDTO material)
    //{
    //    var uri = $"{remoteServiceBaseUrl}materials/{material.Id}";
    //    try
    //    {
    //        var response = await httpClient.PutAsJsonAsync(uri, material);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error updating material: {ex.Message}");
    //        return false;
    //    }
    //}

    //public async Task<bool> DeleteMaterial(int id)
    //{
    //    var uri = $"{remoteServiceBaseUrl}materials/{id}";
    //    try
    //    {
    //        var response = await httpClient.DeleteAsync(uri);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error deleting material: {ex.Message}");
    //        return false;
    //    }
    //}

    //// Metals
    //public async Task<MetalDTO[]> GetMetals()
    //{
    //    var uri = $"{remoteServiceBaseUrl}metals";
    //    var result = await httpClient.GetFromJsonAsync<MetalDTO[]>(uri);
    //    return result ?? Array.Empty<MetalDTO>();
    //}

    //public async Task<MetalDTO> GetMetalById(int id)
    //{
    //    var uri = $"{remoteServiceBaseUrl}metals/{id}";
    //    var result = await httpClient.GetFromJsonAsync<MetalDTO>(uri);
    //    return result!;
    //}

    //public async Task<bool> AddMetal(MetalDTO metal)
    //{
    //    var uri = $"{remoteServiceBaseUrl}metals";
    //    try
    //    {
    //        var response = await httpClient.PostAsJsonAsync(uri, metal);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error creating metal: {ex.Message}");
    //        return false;
    //    }
    //}

    //public async Task<bool> UpdateMetal(MetalDTO metal)
    //{
    //    var uri = $"{remoteServiceBaseUrl}metals/{metal.id}";
    //    try
    //    {
    //        var response = await httpClient.PutAsJsonAsync(uri, metal);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error updating metal: {ex.Message}");
    //        return false;
    //    }
    //}

    //public async Task<bool> DeleteMetal(int id)
    //{
    //    var uri = $"{remoteServiceBaseUrl}metals/{id}";
    //    try
    //    {
    //        var response = await httpClient.DeleteAsync(uri);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error deleting metal: {ex.Message}");
    //        return false;
    //    }
    //}

    //// Occasions
    //public async Task<OccasionDTO[]> GetOccasions()
    //{
    //    var uri = $"{remoteServiceBaseUrl}occasions";
    //    var result = await httpClient.GetFromJsonAsync<OccasionDTO[]>(uri);
    //    return result ?? Array.Empty<OccasionDTO>();
    //}

    //public async Task<OccasionDTO> GetOccasionById(int id)
    //{
    //    var uri = $"{remoteServiceBaseUrl}occasions/{id}";
    //    var result = await httpClient.GetFromJsonAsync<OccasionDTO>(uri);
    //    return result!;
    //}

    //public async Task<bool> AddOccasion(OccasionDTO occasion)
    //{
    //    var uri = $"{remoteServiceBaseUrl}occasions";
    //    try
    //    {
    //        var response = await httpClient.PostAsJsonAsync(uri, occasion);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error creating occasion: {ex.Message}");
    //        return false;
    //    }
    //}

    //public async Task<bool> UpdateOccasion(OccasionDTO occasion)
    //{
    //    var uri = $"{remoteServiceBaseUrl}occasions/{occasion.Id}";
    //    try
    //    {
    //        var response = await httpClient.PutAsJsonAsync(uri, occasion);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error updating occasion: {ex.Message}");
    //        return false;
    //    }
    //}

    //public async Task<bool> DeleteOccasion(int id)
    //{
    //    var uri = $"{remoteServiceBaseUrl}occasions/{id}";
    //    try
    //    {
    //        var response = await httpClient.DeleteAsync(uri);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error deleting occasion: {ex.Message}");
    //        return false;
    //    }
    //}

    //// Styles
    //public async Task<StyleDTO[]> GetStyles()
    //{
    //    var uri = $"{remoteServiceBaseUrl}styles";
    //    var result = await httpClient.GetFromJsonAsync<StyleDTO[]>(uri);
    //    return result ?? Array.Empty<StyleDTO>();
    //}

    //public async Task<StyleDTO> GetStyleById(int id)
    //{
    //    var uri = $"{remoteServiceBaseUrl}styles/{id}";
    //    var result = await httpClient.GetFromJsonAsync<StyleDTO>(uri);
    //    return result!;
    //}

    //public async Task<bool> AddStyle(StyleDTO style)
    //{
    //    var uri = $"{remoteServiceBaseUrl}styles";
    //    try
    //    {
    //        var response = await httpClient.PostAsJsonAsync(uri, style);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error creating style: {ex.Message}");
    //        return false;
    //    }
    //}

    //public async Task<bool> UpdateStyle(StyleDTO style)
    //{
    //    var uri = $"{remoteServiceBaseUrl}styles/{style.Id}";
    //    try
    //    {
    //        var response = await httpClient.PutAsJsonAsync(uri, style);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error updating style: {ex.Message}");
    //        return false;
    //    }
    //}

    //public async Task<bool> DeleteStyle(int id)
    //{
    //    var uri = $"{remoteServiceBaseUrl}styles/{id}";
    //    try
    //    {
    //        var response = await httpClient.DeleteAsync(uri);
    //        response.EnsureSuccessStatusCode();
    //        return true;
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        Console.WriteLine($"Error deleting style: {ex.Message}");
    //        return false;
    //    }
    //}

    //// Manufactures (read-only)
    //public async Task<ManufactureDTO[]> GetManufactures()
    //{
    //    var uri = $"{remoteServiceBaseUrl}manufactures";
    //    var result = await httpClient.GetFromJsonAsync<ManufactureDTO[]>(uri);
    //    return result ?? Array.Empty<ManufactureDTO>();
    //}
}
