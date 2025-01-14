namespace Catalog.Infrastructure;
public record CompositeFilterDto
{
    public CompositeFilterDto(
         decimal minWeight,
         decimal maxWeight,
         int[] productTypes,
         int[] materials,
         int[] metals,
         string sizes,
         int[] occasions,
         int[] styles)
    {
        MinWeight = minWeight;
        MaxWeight = maxWeight;
        ProductTypes = productTypes;
        Materials = materials;
        Metals = metals;
        Sizes = sizes;
        Occasions = occasions;
        Styles = styles;
    }

    // Weight range for filtering
    public decimal MinWeight { get; set; }
    public decimal MaxWeight { get; set; }

    // Array of product type IDs
    public int[] ProductTypes { get; set; }

    // Array of material IDs
    public int[] Materials { get; set; }

    // Array of metal IDs
    public int[] Metals { get; set; }

    // Size filter (if applicable, otherwise empty)
    public string Sizes { get; set; }

    // Array of occasion IDs
    public int[] Occasions { get; set; }

    // Array of style IDs
    public int[] Styles { get; set; }
}