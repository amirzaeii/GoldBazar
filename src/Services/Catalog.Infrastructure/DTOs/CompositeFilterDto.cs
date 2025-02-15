namespace Catalog.Infrastructure;
//public CompositeFilterDto(
//    decimal minWeight,
//    decimal maxWeight,
//    int[] productTypes,
//    int[] materials,
//    int[] metals,
//    string sizes,
//    int[] occasions,
//    int[] styles)
//{
//    MinWeight = minWeight;
//    MaxWeight = maxWeight;
//    ProductTypes = productTypes;
//    Materials = materials;
//    Metals = metals;
//    Sizes = sizes;
//    Occasions = occasions;
//    Styles = styles;
//}

//public decimal MinWeight { get; set; }
//public decimal MaxWeight { get; set; }
//public int[] ProductTypes { get; set; }
//public int[] Materials { get; set; }
//public int[] Metals { get; set; }
//public string Sizes { get; set; }
//public int[] Occasions { get; set; }
//public int[] Styles { get; set; }
//    }
public class CompositeFilterDto
{
    public decimal MinWeight { get; set; }
    public decimal MaxWeight { get; set; }
    public List<int> Metals { get; set; } = new();
    public List<int> Styles { get; set; } = new();
    public List<int> Occasions { get; set; } = new();
    public List<int> Materials { get; set; } = new();
    public List<int> ProductTypes { get; set; } = new();


    // Parameterless constructor for deserialization and default initialization
    public CompositeFilterDto() { }

    // Constructor for easy initialization
    public CompositeFilterDto(
        decimal minWeight,
        decimal maxWeight,
        int[] productTypes,
        int[] materials,
        int[] metals,
        int[] occasions,
        int[] styles)
    {
        MinWeight = minWeight;
        MaxWeight = maxWeight;
        ProductTypes = productTypes != null ? new List<int>(productTypes) : new List<int>();
        Materials = materials != null ? new List<int>(materials) : new List<int>();
        Metals = metals != null ? new List<int>(metals) : new List<int>();
        Occasions = occasions != null ? new List<int>(occasions) : new List<int>();
        Styles = styles != null ? new List<int>(styles) : new List<int>();
    }
}