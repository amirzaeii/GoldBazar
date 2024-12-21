

namespace Catalog.Infrastructure;

public record ProductDto
{
    public int Id { get; }
    public string Caption { get;} = string.Empty;
    public bool ActivityStatus { get; }
    public bool HasOffer { get; }
    public decimal Discount { get;}
    public decimal Weight { get; set; }
    public string ProductType { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public string Metal { get; set; } = string.Empty;
    public int Size { get; set; }
    public string Occasion { get; set; } = string.Empty;
    public string Style { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal CostPerGram { get; set; }
    public int Quantity { get; set; }
    public int ShopId { get; set; }
    public string MainImage { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;

    public ProductDto(Product product)
    {
        Id = product.Id;
        Caption = product.Caption;
        ActivityStatus = product.ActivityStatus;
        HasOffer = product.HasOffer;
        Discount = product.Discount;
        Weight = product.Weight;
        Description = product.Description;
        CostPerGram = product.CostPerGram;
        Quantity = product.Quantity;
        ShopId = product.ShopId;

        // Mapped properties from related entities
        ProductType = product.Type?.Name ?? string.Empty;
        Material = product.Material?.Name ?? string.Empty;
        Metal = product.Metal?.Name ?? string.Empty;
        Occasion = product.Occassion?.Name ?? string.Empty;
        Style = product.Style?.Name ?? string.Empty;
    }

}
