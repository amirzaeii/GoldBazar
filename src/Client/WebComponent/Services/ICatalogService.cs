using Catalog.Infrastructure;
using WebComponent.Dtos;
namespace WebComponent.Services;

public interface ICatalogService
{
    Task<CatalogItem> GetCatalogItem(int id);
    Task<IEnumerable<CatalogItem>> GetCatalogItems(IEnumerable<int> ids);
    Task<CatalogResult> GetCatalogItems(int pageIndex, int pageSize, int? type);
    Task<IEnumerable<CatalogItemType>> GetTypes();
    Task<CatalogResult> GetCatalogItemsByType(int typeId, int pageIndex, int pageSize);
    Task<IEnumerable<CatalogItem>> GetDiscountedCatalogItems(int pageIndex, int pageSize);
    Task<IEnumerable<CatalogItem>> GetSimilarProducts(int typeId);
    Task<bool> AddProduct(Item newItem);
    Task<IEnumerable<CatalogInfo>> GetCatalogDataAsync(string category);
    Task<IEnumerable<CatalogItem>> FilterByComposite(Dtos.CompositeFilterDto filterDto);
    Task<IEnumerable<MaterialType>> GetMaterials();
    Task<IEnumerable<MetalType>> GetMetals();
    Task<IEnumerable<OccasionType>> GetOccasions();
    Task<IEnumerable<StyleType>> GetStyles();
}
