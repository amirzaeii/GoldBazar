
namespace GoldBazar.Shared.Components.Services;

public interface IProductImageUrlProvider
{
    string GetProductImageUrl(ItemDTO item)
        => GetProductImageUrl(item.Id);

    string GetProductImageUrl(int productId);
    string GetTypeImageUrl(ItemCategoryDTO type)
        => GetTypeImageUrl(type.Id);
    string GetTypeImageUrl(int typeId);

}
