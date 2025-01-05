namespace Catalog.Infrastructure;
public record CompositeFilterDto
{
    public CompositeFilterDto(decimal minWeight, decimal maxWeight, int[] productType, int[] material, int[] metal, string size, int[] occasion, int[] style, int[] shopId, int[] city, int pageIndex, int pageSize, string sortField, string sortOrder)
    {
        MinWeight = minWeight;
        MaxWeight = maxWeight;
        ProductType = productType;
        Material = material;
        Metal = metal;
        Size = size;
        Occasion = occasion;
        Style = style;
        ShopId = shopId;
        City = city;
        PageIndex = pageIndex;
        PageSize = pageSize;
        SortField = sortField;
        SortOrder = sortOrder;
    }

    public decimal MinWeight { get; set; }
    public decimal MaxWeight { get; set; }
    public int[] ProductType { get; set; }
    public int[] Material { get; set; } 
    public int[] Metal { get; set; }
    public string Size { get; set; } 
    public int[] Occasion { get; set; } 
    public int[] Style { get; set; } 
    public int[] ShopId { get; set; } 
    public int[] City { get; set; }  
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortField { get; set; } = "Caption";
    public string SortOrder { get; set; } = "asc";
}
