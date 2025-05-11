using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.AspNetCore.Components.Forms;
namespace GoldBazar.Shared.Components.Services;

public class CatalogService(HttpClient httpClient) 
{
    private readonly string remoteServiceBaseUrl = "api/catalog/";

    public async Task<ItemDTO> GetCatalogItem(int id)
    {
        var uri = $"{remoteServiceBaseUrl}items/{id}";
        var result =  await httpClient.GetFromJsonAsync<ItemDTO>(uri);
        return result!;
    }
    public async Task<ItemDTO[]> GetCatalogItems(IEnumerable<int> ids)
    {
        var uri = $"{remoteServiceBaseUrl}items/by?ids={string.Join("&ids=", ids)}";
        var result = await httpClient.GetFromJsonAsync<ItemDTO[]>(uri);
        return result!;
    }
    public async Task<ItemDTO> GetCatalogItems(int pageIndex, int pageSize, int? type)
    {
        var uri = GetAllCatalogItemsUri(remoteServiceBaseUrl, pageIndex, pageSize, type);
        var result = await httpClient.GetFromJsonAsync<ItemDTO>(uri);
        return result!;
    }

    public async Task<ItemCategoryDTO[]> GetItemCatergories()
    {
        var uri = $"{remoteServiceBaseUrl}categories";
        var result = await httpClient.GetFromJsonAsync<ItemCategoryDTO[]>(uri);
        return result!;
    }
    public async Task<ManufactureDTO[]> GetManufactures()
    {
        var uri = $"{remoteServiceBaseUrl}manufactures";
        var result = await httpClient.GetFromJsonAsync<ManufactureDTO[]>(uri);
        return result!;
    }
    
    public async Task<MaterialDTO[]> GetMaterials()
    {
        var uri = $"{remoteServiceBaseUrl}materials";
        var result = await httpClient.GetFromJsonAsync<MaterialDTO[]>(uri);
        return result!;
    }
    
    public async Task<MetalDTO[]> GetMetals()
    {
        var uri = $"{remoteServiceBaseUrl}metals";
        var result = await httpClient.GetFromJsonAsync<MetalDTO[]>(uri);
        return result!;
    }
    
    public async Task<OccasionDTO[]> GetOccasions()
    {
        var uri = $"{remoteServiceBaseUrl}occasions";
        var result = await httpClient.GetFromJsonAsync<OccasionDTO[]>(uri);
        return result!;
    }
    
    public async Task<StyleDTO[]> GetStyles()
    {
        var uri = $"{remoteServiceBaseUrl}styles";
        var result = await httpClient.GetFromJsonAsync<StyleDTO[]>(uri);
        return result!;
    }

    private static string GetAllCatalogItemsUri(string baseUri, int pageIndex, int pageSize, int? type)
    {
        return $"{baseUri}items?pageIndex={pageIndex}&pageSize={pageSize}";
    }

    public async Task<ItemResult> GetCatalogItemsByType(int pageIndex, int pageSize, int typeId)
    {
        var uri = $"{remoteServiceBaseUrl}items/type/{typeId}?pageIndex={pageIndex}&pageSize={pageSize}";

        // Deserialize as array of CatalogItem
        var items = await httpClient.GetFromJsonAsync<ItemDTO[]>(uri);

        items ??= Array.Empty<ItemDTO>();

        var paginatedItems = items
        .Skip(pageIndex * pageSize)
        .Take(pageSize)
        .ToList();
        // Wrap in CatalogResult
        var result = new ItemResult(
            pageIndex,
            pageSize,
            items.Length,
            paginatedItems
        );

        return result;
    }
    public async Task<IEnumerable<ItemDTO>> GetDiscountedCatalogItems(int pageIndex, int pageSize)
    {
        var uri = $"{remoteServiceBaseUrl}items/discounted?";

        try
        {
            var result = await httpClient.GetFromJsonAsync<ItemDTO[]>(uri);
            return result ?? Array.Empty<ItemDTO>();
        }
        catch (HttpRequestException)
        {
            // Log the error or handle it as needed
            return Array.Empty<ItemDTO>();
        }
    }


    public async Task<IEnumerable<ItemDTO>> GetSimilarProducts(int typeId)
    {
        var uri = $"{remoteServiceBaseUrl}item/similar/{typeId}";

        try
        {
            var result = await httpClient.GetFromJsonAsync<IEnumerable<ItemDTO>>(uri);
            return result ?? Array.Empty<ItemDTO>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error fetching similar products: {ex.Message}");
            return Array.Empty<ItemDTO>();
        }
    }
    public async Task<bool> AddItem(ItemDTO newItem)
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
            Console.WriteLine($"Error adding an item: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> EditItem(ItemDTO newItem)
    {
        var uri = "api/catalog/item"; // Ensure this matches your API

        try
        {
            Console.WriteLine($"Sending request to: {uri}");
            Console.WriteLine($"Payload: {JsonSerializer.Serialize(newItem)}");

            var response = await httpClient.PutAsJsonAsync(uri, newItem);

            Console.WriteLine($"Response status: {response.StatusCode}");

            response.EnsureSuccessStatusCode(); // Throws exception if not 2xx

            return true;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error editing an item: {ex.Message}");
            return false;
        }
    }
     public async Task<string> UploadItemImage(IBrowserFile file)
    {
        var uri = "api/catalog/item/pic"; 

        try
        {
            var fileContent = new StreamContent(file.OpenReadStream(1024 * 1024 * 10));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://catalog-api/api/catalog/item/pic"),
                Content = new MultipartFormDataContent
                {
                    // Add the file content to the request
                     {fileContent, "file", file.Name }
                },
            };
          
            Console.WriteLine($"Sending request to: {uri}");

            // var _httpClient = new HttpClient();
            // _httpClient.BaseAddress = new Uri("http://catalog-api");
            var response = await httpClient.SendAsync(request);

            Console.WriteLine($"Response status: {response.StatusCode}");

            response.EnsureSuccessStatusCode(); // Throws exception if not 2xx

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error adding upload image: {ex.Message}");
            return "default.jpg";
        }
    }

    public async Task<IEnumerable<CatalogInfo>> GetCatalogDataAsync(string category)
    {
        var uri = $"{remoteServiceBaseUrl}{category}";
        var result = await httpClient.GetFromJsonAsync<IEnumerable<CatalogInfo>>(uri);
        return result ?? Array.Empty<CatalogInfo>();
    }

    public async Task<IEnumerable<ItemDTO>> FilterByComposite(CompositeFilterDto filterDto)
    {
        var uri = $"{remoteServiceBaseUrl}item/filter";
        try
        {
            var response = await httpClient.PostAsJsonAsync(uri, filterDto);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<ItemDTO>>();
            return result ?? Array.Empty<ItemDTO>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error filtering catalog items: {ex.Message}");
            return Array.Empty<ItemDTO>();
        }
    }
}

