namespace WebComponent.Dtos;
public record CatalogItem(
    int Id, 
    string Caption, 
    string Description,
    decimal CostPerGram,
    decimal Weight,
    int Size,
    int TypeId,
    string TypeName,
    int MetalId,
    string MetalName,
    decimal KT,
    int ShopId,
    string ShopName,
    int MaterialId,
    string MaterailName,
    int OccasionId,
    string OccasionName,
    int StyleId,
    string StyleName,
    decimal Discount,
    bool Status,
    decimal Price,
    decimal OldPrice,
    //Added
    string ImageUrl,
    decimal Rating,
    int RatingCount,
    int Quantity,
    CatalogItemType CatalogType);

public record CatalogResult(int PageIndex, int PageSize, int Count, List<CatalogItem> Data);
public record CatalogItemType(int Id, string Name);
public record MaterialType(int Id, string Name);
public record MetalType(int Id, string Name, int manufacture, int kt, double purity);
public record OccasionType(int Id, string Name);
public record StyleType(int Id, string Name);


public class CompositeFilterDto
{
    public decimal MinWeight { get; set; }
    public decimal MaxWeight { get; set; }
    public List<int> Metals { get; set; } = new();
    public List<int> Styles { get; set; } = new();
    public List<int> Occasions { get; set; } = new();
    public List<int> Materials { get; set; } = new();
    public List<int> ProductTypes { get; set; } = new();
}