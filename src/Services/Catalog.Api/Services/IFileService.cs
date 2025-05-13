using GoldBazar.Shared.DTOs;

namespace Catalog.Api.Services;
public interface IFileService
{
    Task<UploadResult> SaveFileAsync(IFormFile file);
}