using GoldBazar.Shared.DTOs;

namespace Catalog.Api.Services;

public class LocalFileService : IFileService
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<LocalFileService> _logger;
    public LocalFileService(IWebHostEnvironment env, ILogger<LocalFileService> logger)
    {
        _env = env;
        _logger = logger;
    }

    public async Task<UploadResult> SaveFileAsync(IFormFile file)
    {
        var maxAllowedFiles = 4;
        long maxFileSize = 1024 * 500 ; // 500 KB
        var filesProcessed = 0;

            var uploadResult = new UploadResult();
            string trustedFileNameForFileStorage;
            var untrustedFileName = file.FileName;
            uploadResult.FileName = untrustedFileName;
            var trustedFileNameForDisplay = untrustedFileName;

            if (filesProcessed < maxAllowedFiles)
            {
                if (file.Length == 0)
                {
                    _logger.LogInformation("{FileName} length is 0 (Err: 1)",
                        trustedFileNameForDisplay);
                    uploadResult.ErrorCode = 1;
                }
                else if (file.Length > maxFileSize)
                {
                    _logger.LogInformation("{FileName} of {Length} bytes is " +
                        "larger than the limit of {Limit} bytes (Err: 2)",
                        trustedFileNameForDisplay, file.Length, maxFileSize);
                    uploadResult.ErrorCode = 2;
                }
                else
                {
                    try
                    {
                        trustedFileNameForFileStorage = Path.GetRandomFileName();
                        var path = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "items",Path.GetFileNameWithoutExtension(trustedFileNameForFileStorage) + Path.GetExtension(file.FileName));

                        await using FileStream fs = new(path, FileMode.Create);
                        await file.CopyToAsync(fs);

                        _logger.LogInformation("{FileName} saved at {Path}",
                            trustedFileNameForDisplay, path);
                        uploadResult.Uploaded = true;
                        uploadResult.StoredFileName = trustedFileNameForFileStorage;
                    }
                    catch (IOException ex)
                    {
                        _logger.LogError("{FileName} error on upload (Err: 3): {Message}",
                            trustedFileNameForDisplay, ex.Message);
                        uploadResult.ErrorCode = 3;
                    }
                }

                filesProcessed++;
            }
            else
            {
                _logger.LogInformation("{FileName} not uploaded because the " +
                    "request exceeded the allowed {Count} of files (Err: 4)",
                    trustedFileNameForDisplay, maxAllowedFiles);
                uploadResult.ErrorCode = 4;
            }

        return uploadResult;
    }
}
