namespace Catalog.Api.Services;
public interface IStorageService
{
    Task<string> UploadFileAsync(IFormFile file);
}