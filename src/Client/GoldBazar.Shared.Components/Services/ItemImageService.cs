using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Forms;
namespace GoldBazar.Shared.Components.Services;

public class ItemImageService(HttpClient httpClient) : IItemImageService
{
    public async Task<string> UploadItemImage(IBrowserFile file)
    {
        if (file == null)
            return "default.jpg";

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/catalog/item/pic");
            var content = new MultipartFormDataContent
            {
                { new StreamContent(File.OpenRead("/Users/abbas/Downloads/GB/Image/types/Baraq.png")), "file", "Belt.png" }
               // {new StreamContent(file.OpenReadStream(file.Size)), "file", file.Name}
            };
            request.Content = content;
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading file: {ex.Message}");
            return string.Empty;   
        }
    }
    public string GetProductImageUrl(int productId)
        => $"product-images/{productId}?api-version=1.0";
    public string GetTypeImageUrl(int typeId)
        => $"type-images/{typeId}?api-version=1.0";

}