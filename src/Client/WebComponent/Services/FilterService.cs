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
        //private readonly string remoteServiceBaseUrl = "api/catalogInfo/";

        //// Get All Materials
        //public async Task<IEnumerable<CatalogInfo>> GetAllMaterials()
        //{
        //    var uri = $"{remoteServiceBaseUrl}materials";
        //    var result = await httpClient.GetFromJsonAsync<IEnumerable<CatalogInfo>>(uri);
        //    return result ?? Array.Empty<CatalogInfo>();
        //}

        //// Get All Metals
        //public async Task<IEnumerable<CatalogInfo>> GetAllMetals()
        //{
        //    var uri = $"{remoteServiceBaseUrl}metals";
        //    var result = await httpClient.GetFromJsonAsync<IEnumerable<CatalogInfo>>(uri);
        //    return result ?? Array.Empty<CatalogInfo>();
        //}

        //// Get All Occasions
        //public async Task<IEnumerable<CatalogInfo>> GetAllOccasions()
        //{
        //    var uri = $"{remoteServiceBaseUrl}occasions";
        //    var result = await httpClient.GetFromJsonAsync<IEnumerable<CatalogInfo>>(uri);
        //    return result ?? Array.Empty<CatalogInfo>();
        //}

        //// Get All Styles
        //public async Task<IEnumerable<CatalogInfo>> GetAllStyles()
        //{
        //    var uri = $"{remoteServiceBaseUrl}styles";
        //    var result = await httpClient.GetFromJsonAsync<IEnumerable<CatalogInfo>>(uri);
        //    return result ?? Array.Empty<CatalogInfo>();
        //}

        private readonly string remoteServiceBaseUrl = "api/catalogInfo/";

        // Unified fetch method for all categories
        public async Task<IEnumerable<CatalogInfo>> GetCatalogDataAsync(string category)
        {
            var uri = $"{remoteServiceBaseUrl}{category}";
            var result = await httpClient.GetFromJsonAsync<IEnumerable<CatalogInfo>>(uri);
            return result ?? Array.Empty<CatalogInfo>();
        }
    }
}

