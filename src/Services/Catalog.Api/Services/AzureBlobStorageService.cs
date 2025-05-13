

using GoldBazar.Shared.DTOs;

namespace Catalog.Api.Services;

public class AzureBlobStorageService : IFileService
{
    public Task<UploadResult> SaveFileAsync(IFormFile file)
    {
        throw new NotImplementedException();
    }
}
