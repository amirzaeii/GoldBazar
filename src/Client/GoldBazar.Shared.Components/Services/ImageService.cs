using System.Net.Http.Headers;
using System.Text.Json;

using Microsoft.AspNetCore.Components.Forms;
namespace GoldBazar.Shared.Components.Services;

public class ImageService(HttpClient httpClient) : IImageService
{
    public async Task<string> UploadItemImage(IBrowserFile file)
    {
        if (file == null)
            return "default.jpg";
        bool upload = false;
        var content = new MultipartFormDataContent();       
        try
        {                   
           // var newFile = await ResizeImage(file);     
             var memoryStream = new MemoryStream();
            using var browserStream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10 MB limit
            await browserStream.CopyToAsync(memoryStream);

            var fileContent = new StreamContent(memoryStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            content.Add(
                content: fileContent,
                name: "file",
                fileName: file.Name);
            
            upload = true;
        }
        catch (Exception ex)
        {                    
             Console.WriteLine($"Error uploading file: {ex.Message}");
        }

        if (upload)
        {
            var response = await httpClient.PostAsync("/api/catalog/item/pic", content);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                using var responseStream = await response.Content.ReadAsStreamAsync();

                var newUploadResults = await JsonSerializer.DeserializeAsync<IList<UploadResult>>(responseStream, options);

                return newUploadResults[0].StoredFileName;
            }
        }
        return "default.jpg";
    }
    public string GetProductImageUrl(int productId)
        => $"product-images/{productId}?api-version=1.0";
    public string GetTypeImageUrl(int typeId)
        => $"type-images/{typeId}?api-version=1.0";
    public string GetShopLogoUrl(int shopId)
        => $"shop-logo/{shopId}?api-version=1.0";
    public string GetShopBannerUrl(int shopId)
        => $"shop-banner/{shopId}?api-version=1.0";

}