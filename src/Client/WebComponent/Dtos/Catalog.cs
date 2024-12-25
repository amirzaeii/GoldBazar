namespace WebComponent.Dtos;
public record CatalogItem(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string PictureUrl,
    int CatalogTypeId,
    CatalogItemType CatalogType);

public record CatalogResult(int PageIndex, int PageSize, int Count, List<CatalogItem> Data);
public record CatalogItemType(int Id, string Name, string PictureUrl);
