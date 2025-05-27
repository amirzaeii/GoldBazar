using Microsoft.AspNetCore.Components.Forms;

namespace GoldBazar.Admin.Web.Components;

public record FileUploadDTO
{
    public IBrowserFile File { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }
    public string PreviewUrl { get; set; }
}