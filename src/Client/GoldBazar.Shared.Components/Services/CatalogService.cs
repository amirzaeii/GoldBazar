using System.Net.Http.Json;
using System.Text.Json;
namespace GoldBazar.Shared.Components.Services;

public class CatalogService(HttpClient httpClient)
{
    private readonly string remoteServiceBaseUrl = "api/catalog/";

    public async Task<ItemDTO> GetCatalogItem(int id)
    {
        var uri = $"{remoteServiceBaseUrl}items/{id}";
        var result = await httpClient.GetFromJsonAsync<ItemDTO>(uri);
        return result!;
    }
    public async Task<ItemDTO[]> GetCatalogItems(IEnumerable<int> ids)
    {
        var uri = $"{remoteServiceBaseUrl}items/by?ids={string.Join("&ids=", ids)}";
        var result = await httpClient.GetFromJsonAsync<ItemDTO[]>(uri);
        return result!;
    }
    public async Task<ItemResult> GetCatalogItems(int pageIndex, int pageSize, int? type)
    {
        var uri = $"{remoteServiceBaseUrl}items?pageIndex={pageIndex}&pageSize={pageSize}";
        var result = await httpClient.GetFromJsonAsync<ItemResult>(uri);
        return result!;
    }

    public async Task<ItemPhotosDTO[]> GetItemPhotos(int itemId)
    {
        var uri = $"{remoteServiceBaseUrl}items/{itemId}/pic";
        var result = await httpClient.GetFromJsonAsync<ItemPhotosDTO[]>(uri);
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
        var uri = "api/catalog/item/"; // Ensure this matches your API

        try
        {
            Console.WriteLine($"Sending request to: {uri}");
            Console.WriteLine($"Payload: {JsonSerializer.Serialize(newItem)}");

            var response = await httpClient.PutAsJsonAsync($"{uri}{newItem.Id}", newItem);

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

    /***********************************************************************************/

    // Metals
    public async Task<MetalDTO[]> GetMetals()
    {
        var uri = $"{remoteServiceBaseUrl}metals";
        var result = await httpClient.GetFromJsonAsync<MetalDTO[]>(uri);
        return result ?? Array.Empty<MetalDTO>();
    }

    public async Task<MetalDTO?> GetMetalById(int id)
    {
        var uri = $"{remoteServiceBaseUrl}metals/{id}";
        return await httpClient.GetFromJsonAsync<MetalDTO>(uri);
    }

    public async Task<MetalDTO?> AddMetal(MetalDTO dto)
    {
        var uri = $"{remoteServiceBaseUrl}metals";
        var response = await httpClient.PostAsJsonAsync(uri, dto);
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<MetalDTO>();
    }
    public async Task<MetalDTO?> UpdateMetal(MetalDTO dto)
    {
        var uri = $"{remoteServiceBaseUrl}metals/{dto.Id}";
        var response = await httpClient.PutAsJsonAsync(uri, dto);
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<MetalDTO>();
    }
    public async Task<bool> DeleteMetal(int id)
    {
        var uri = $"{remoteServiceBaseUrl}metals/{id}";
        var response = await httpClient.DeleteAsync(uri);
        return response.IsSuccessStatusCode;
    }


    // Occasions
    // --------------------------
    // Occasions (ViewModel-style)
    // --------------------------

    // GET /api/catalog/occasions
    public async Task<OccasionDTO[]> GetOccasions()
    {
        var uri = $"{remoteServiceBaseUrl}occasions";
        var result = await httpClient.GetFromJsonAsync<OccasionDTO[]>(uri);
        return result ?? Array.Empty<OccasionDTO>();
    }

    // GET /api/catalog/occasions/{id}
    public Task<OccasionDTO> GetOccasionById(int id)
        => httpClient.GetFromJsonAsync<OccasionDTO>($"{remoteServiceBaseUrl}occasions/{id}")!;

    // POST /api/catalog/occasions
    public async Task<OccasionDTO?> AddOccasion(OccasionDTO newOccasion)
    {
        var resp = await httpClient.PostAsJsonAsync(
            $"{remoteServiceBaseUrl}occasions", newOccasion);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<OccasionDTO>()!;
    }

    // PUT /api/catalog/occasions/{id}
    public async Task<OccasionDTO?> UpdateOccasion(OccasionDTO occasion)
    {
        var resp = await httpClient.PutAsJsonAsync(
            $"{remoteServiceBaseUrl}occasions/{occasion.Id}", occasion);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<OccasionDTO>()!;
    }

    // DELETE /api/catalog/occasions/{id}
    public async Task<bool> DeleteOccasion(int id)
        => (await httpClient.DeleteAsync($"{remoteServiceBaseUrl}occasions/{id}"))
           .IsSuccessStatusCode;


    /******************************************/

    //// Styles
    // GET /api/catalog/styles
    public async Task<StyleDTO[]> GetStyles()
    {
        var uri = $"{remoteServiceBaseUrl}styles";
        var result = await httpClient.GetFromJsonAsync<StyleDTO[]>(uri);
        return result!;
    }

    // GET /api/catalog/styles/{id}
    public Task<StyleDTO> GetStyleById(int id)
        => httpClient.GetFromJsonAsync<StyleDTO>($"{remoteServiceBaseUrl}styles/{id}")!;

    // POST /api/catalog/styles
    public async Task<StyleDTO?> AddStyle(StyleDTO newStyle)
    {
        var resp = await httpClient.PostAsJsonAsync<StyleDTO>(
            $"{remoteServiceBaseUrl}styles",
            newStyle);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<StyleDTO>()!;
    }

    // PUT /api/catalog/styles/{id}
    public async Task<StyleDTO?> EditStyle(StyleDTO style)
    {
        var resp = await httpClient.PutAsJsonAsync(
            $"{remoteServiceBaseUrl}styles/{style.Id}",
            style);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<StyleDTO>()!;
    }

    // DELETE /api/catalog/styles/{id}
    public async Task<bool> DeleteStyle(int id)
        => (await httpClient.DeleteAsync(
                $"{remoteServiceBaseUrl}styles/{id}"))
           .IsSuccessStatusCode;


    // Materials

    // GET /api/catalog/materials
    public async Task<MaterialDTO[]> GetMaterials()
    {
        var uri = $"{remoteServiceBaseUrl}materials";
        var result = await httpClient.GetFromJsonAsync<MaterialDTO[]>(uri);
        return result ?? Array.Empty<MaterialDTO>();
    }

    // GET /api/catalog/materials/{id}
    public Task<MaterialDTO> GetMaterialById(int id)
        => httpClient.GetFromJsonAsync<MaterialDTO>($"{remoteServiceBaseUrl}materials/{id}")!;

    // POST /api/catalog/materials
    public async Task<MaterialDTO?> AddMaterial(MaterialDTO newMaterial)
    {
        var resp = await httpClient.PostAsJsonAsync(
            $"{remoteServiceBaseUrl}materials", newMaterial);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<MaterialDTO>()!;
    }

    // PUT /api/catalog/materials/{id}
    public async Task<MaterialDTO?> UpdateMaterial(MaterialDTO material)
    {
        var resp = await httpClient.PutAsJsonAsync(
            $"{remoteServiceBaseUrl}materials/{material.Id}", material);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<MaterialDTO>()!;
    }

    // DELETE /api/catalog/materials/{id}
    public async Task<bool> DeleteMaterial(int id)
        => (await httpClient.DeleteAsync($"{remoteServiceBaseUrl}materials/{id}"))
           .IsSuccessStatusCode;


}

