using WebComponent.Dtos;

namespace WebComponent.Services;

public interface ICatalogService
{
    Task<CatalogItem?> GetCatalogItem(int id);
    Task<CatalogResult> GetCatalogItems(int pageIndex, int pageSize, int? type);
    Task<IEnumerable<CatalogItemType>> GetTypes();
    Task<CatalogResult> GetCatalogItemsByType(int typeId, int pageIndex, int pageSize);
    Task<IEnumerable<CatalogItem>> GetDiscountedCatalogItems(int pageIndex, int pageSize);
    Task<IEnumerable<CatalogItem>> FilterCatalogItems(CompositeFilterDto filterDto);
    Task<IEnumerable<CatalogItem>> GetSimilarProducts(int typeId);
}
