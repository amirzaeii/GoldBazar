using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Infrastructure.Models;

public class ItemPhoto
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string PhotoPath { get; set; } = string.Empty;
    public string ThumbnailPath { get; set; } = string.Empty;
    public int Priority { get; set; }
    public string Description { get; set; } = string.Empty;
    public int ItemId { get; set; }
    [ForeignKey("ItemId")]
    public Item Item { get; set; } = default!;
}
