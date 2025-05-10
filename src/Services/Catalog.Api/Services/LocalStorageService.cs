namespace Catalog.Api.Services;

public class LocalStorageService : IStorageService
{
    private readonly IWebHostEnvironment _env;
    public LocalStorageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var folder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "items");
        Directory.CreateDirectory(folder);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(folder, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return fileName;
    }
}
