using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using WebComponent.Dtos;

namespace WebComponent.Services;


public class CatalogService(HttpClient httpClient) : ICatalogService
{
    private readonly string remoteServiceBaseUrl = "api/catalog/";

    public Task<CatalogItem?> GetCatalogItem(int id)
    {
        var uri = $"{remoteServiceBaseUrl}items/{id}";
        return httpClient.GetFromJsonAsync<CatalogItem>(uri);
    }
    public async Task<CatalogResult> GetCatalogItems(int pageIndex, int pageSize, int? type)
    {
        var uri = GetAllCatalogItemsUri(remoteServiceBaseUrl, pageIndex, pageSize, type);
        var result = await httpClient.GetFromJsonAsync<CatalogResult>(uri);
        return result!;
    }

    public async Task<IEnumerable<CatalogItemType>> GetTypes()
    {
        var uri = $"{remoteServiceBaseUrl}types";
        var result = await httpClient.GetFromJsonAsync<CatalogItemType[]>(uri);
        return result!;
    }

    private static string GetAllCatalogItemsUri(string baseUri, int pageIndex, int pageSize, int? type)
    {
        return $"{baseUri}items?pageIndex={pageIndex}&pageSize={pageSize}";
    }

}
