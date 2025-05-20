public class Product
{
    public required string Name { get; set; }
    public required string Image { get; set; }
    public required string Price { get; set; }
    public string? DiscountPrice { get; set; }
    public bool HasDiscountBadge { get; set; }
    public bool IsFavorite { get; set; }
    public required Category Category { get; set; }
}
