using WebComponent.Dtos;

namespace WebComponent.Services;

public interface IProductImageUrlProvider
{
    string GetProductImageUrl(CatalogItem item)
        => GetProductImageUrl(item.Id);

    string GetProductImageUrl(int productId);
    string GetTypeImageUrl(CatalogItemType type)
        => GetTypeImageUrl(type.Id);
    string GetTypeImageUrl(int typeId);

}
