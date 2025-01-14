using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebComponent.Dtos;

namespace WebComponent.Services
{
    public class FilterService(HttpClient httpClient) : IFilterService
    {
        private readonly string remoteServiceBaseUrl = "api/catalogInfo/";

        // Unified fetch method for all categories
        public async Task<IEnumerable<CatalogInfo>> GetCatalogDataAsync(string category)
        {
            var uri = $"{remoteServiceBaseUrl}{category}";
            var result = await httpClient.GetFromJsonAsync<IEnumerable<CatalogInfo>>(uri);
            return result ?? Array.Empty<CatalogInfo>();
        }
        public async Task<IEnumerable<CatalogItem>> FilterCatalogItemsAsync(CompositeFilterDto filterDto)
        {
            var uri = "api/catalog/item/filter";

            try
            {
                // Send the DTO to the backend
                var response = await httpClient.PostAsJsonAsync(uri, filterDto);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Deserialize the response
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<CatalogItem>>();
                return result ?? Array.Empty<CatalogItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in API call: {ex.Message}");
                return Array.Empty<CatalogItem>();
            }
        }

    }
}


