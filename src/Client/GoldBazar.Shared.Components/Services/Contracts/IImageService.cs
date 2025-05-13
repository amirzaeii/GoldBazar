using Microsoft.AspNetCore.Components.Forms;

namespace GoldBazar.Shared.Components.Services.Contracts;

public interface IImageService
{
    Task<string> UploadItemImage(IBrowserFile file);
    string GetProductImageUrl(ItemDTO item)
        => GetProductImageUrl(item.Id);

    string GetProductImageUrl(int productId);
    string GetTypeImageUrl(ItemCategoryDTO type)
        => GetTypeImageUrl(type.Id);
    string GetTypeImageUrl(int typeId);

    string GetShopLogoUrl(ShopDTO shop)
        => GetShopLogoUrl(shop.Id);
    string GetShopLogoUrl(int shopId);

    string GetShopBannerUrl(ShopDTO shop)
        => GetShopBannerUrl(shop.Id);
    string GetShopBannerUrl(int shopId);

}
