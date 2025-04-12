using System.Net.Http.Json;
using System.Text.Json;
using Catalog.Infrastructure;
using WebComponent.Dtos;
namespace WebComponent.Services;

public class CatalogService(HttpClient httpClient) : ICatalogService
{
    private readonly string remoteServiceBaseUrl = "api/catalog/";

    public async Task<CatalogItem> GetCatalogItem(int id)
    {
        var uri = $"{remoteServiceBaseUrl}items/{id}";
        var result =  await httpClient.GetFromJsonAsync<CatalogItem>(uri);
        return result!;
    }
    public async Task<IEnumerable<CatalogItem>> GetCatalogItems(IEnumerable<int> ids)
    {
        var uri = $"{remoteServiceBaseUrl}items/by?ids={string.Join("&ids=", ids)}";
        var result = await httpClient.GetFromJsonAsync<List<CatalogItem>>(uri);
        return result!;
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
    
    public async Task<IEnumerable<MaterialType>> GetMaterials()
    {
        var uri = $"{remoteServiceBaseUrl}materials";
        var result = await httpClient.GetFromJsonAsync<MaterialType[]>(uri);
        return result!;
    }
    
    public async Task<IEnumerable<MetalType>> GetMetals()
    {
        var uri = $"{remoteServiceBaseUrl}metals";
        var result = await httpClient.GetFromJsonAsync<MetalType[]>(uri);
        return result!;
    }
    
    public async Task<IEnumerable<OccasionType>> GetOccasions()
    {
        var uri = $"{remoteServiceBaseUrl}occasions";
        var result = await httpClient.GetFromJsonAsync<OccasionType[]>(uri);
        return result!;
    }
    
    public async Task<IEnumerable<StyleType>> GetStyles()
    {
        var uri = $"{remoteServiceBaseUrl}styles";
        var result = await httpClient.GetFromJsonAsync<StyleType[]>(uri);
        return result!;
    }

    private static string GetAllCatalogItemsUri(string baseUri, int pageIndex, int pageSize, int? type)
    {
        return $"{baseUri}items?pageIndex={pageIndex}&pageSize={pageSize}";
    }

    public async Task<CatalogResult> GetCatalogItemsByType(int pageIndex, int pageSize, int typeId)
    {
        var uri = $"{remoteServiceBaseUrl}items/type/{typeId}?pageIndex={pageIndex}&pageSize={pageSize}";

        // Deserialize as array of CatalogItem
        var items = await httpClient.GetFromJsonAsync<CatalogItem[]>(uri);

        items ??= Array.Empty<CatalogItem>();

        var paginatedItems = items
        .Skip(pageIndex * pageSize)
        .Take(pageSize)
        .ToList();
        // Wrap in CatalogResult
        var result = new CatalogResult(
            pageIndex,
            pageSize,
            items.Length,
            paginatedItems
        );

        return result;
    }
    public async Task<IEnumerable<CatalogItem>> GetDiscountedCatalogItems(int pageIndex, int pageSize)
    {
        var uri = $"{remoteServiceBaseUrl}items/discounted?";

        try
        {
            var result = await httpClient.GetFromJsonAsync<CatalogItem[]>(uri);
            return result ?? Array.Empty<CatalogItem>();
        }
        catch (HttpRequestException)
        {
            // Log the error or handle it as needed
            return Array.Empty<CatalogItem>();
        }
    }


    public async Task<IEnumerable<CatalogItem>> GetSimilarProducts(int typeId)
    {
        var uri = $"{remoteServiceBaseUrl}item/similar/{typeId}";

        try
        {
            var result = await httpClient.GetFromJsonAsync<IEnumerable<CatalogItem>>(uri);
            return result ?? Array.Empty<CatalogItem>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching similar products: {ex.Message}");
            return Array.Empty<CatalogItem>();
        }
    }
    public async Task<bool> AddProduct(Item newItem)
    {
        var uri = "api/catalog/item"; // Ensure this matches your API

        try
        {
            Console.WriteLine($"Sending request to: {uri}");
            Console.WriteLine($"Payload: {JsonSerializer.Serialize(newItem)}");

            var response = await httpClient.PostAsJsonAsync(uri, newItem);

            Console.WriteLine($"Response status: {response.StatusCode}");

            response.EnsureSuccessStatusCode(); // Throws exception if not 2xx

            return true;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error adding new product: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<CatalogInfo>> GetCatalogDataAsync(string category)
    {
        var uri = $"{remoteServiceBaseUrl}{category}";
        var result = await httpClient.GetFromJsonAsync<IEnumerable<CatalogInfo>>(uri);
        return result ?? Array.Empty<CatalogInfo>();
    }

    public async Task<IEnumerable<CatalogItem>> FilterByComposite(Dtos.CompositeFilterDto filterDto)
    {
        var uri = $"{remoteServiceBaseUrl}item/filter";
        try
        {
            var response = await httpClient.PostAsJsonAsync(uri, filterDto);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<CatalogItem>>();
            return result ?? Array.Empty<CatalogItem>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error filtering catalog items: {ex.Message}");
            return Array.Empty<CatalogItem>();
        }
    }
}

