namespace GoldBazar.Vendor.Web.Utility;

public record FileUploadDTO
    {
        public IBrowserFile File { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string PreviewUrl { get; set; }
    }