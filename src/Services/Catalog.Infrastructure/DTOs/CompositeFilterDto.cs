
namespace Catalog.Infrastructure;

public class CompositeFilterDto
{
    public decimal MinWeight { get; set; }
    public decimal MaxWeight { get; set; }
    public string ProductType { get; set; }
    public string Material { get; set; }
    public string Metal { get; set; }
    public string Size { get; set; }
    public string Occasion { get; set; }
    public string Style { get; set; }
    public string Manufacturer { get; set; }


    // Constructor to initialize all properties with default values
    public CompositeFilterDto(
        decimal minWeight = 0,
        decimal maxWeight = 0,
        string productType = "",
        string material = "",
        string metal = "",
        string size = "",
        string occasion = "",
        string style = "",
        string manufacturer = "")
    {
        MinWeight = minWeight;
        MaxWeight = maxWeight;
        ProductType = productType;
        Material = material;
        Metal = metal;
        Size = size;
        Occasion = occasion;
        Style = style;
        Manufacturer = manufacturer;
    }
}
