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
    CatalogItemType CatalogType);

public record CatalogResult(int PageIndex, int PageSize, int Count, List<CatalogItem> Data);
public record CatalogItemType(int Id, string Name);
