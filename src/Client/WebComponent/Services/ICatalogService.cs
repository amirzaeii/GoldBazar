using WebComponent.Dtos;

namespace WebComponent.Services;

public interface ICatalogService
{
        Task<CatalogItem?> GetCatalogItem(int id);
        Task<CatalogResult> GetCatalogItems(int pageIndex, int pageSize, int? brand, int? type);
        Task<IEnumerable<CatalogItemType>> GetTypes();
}
