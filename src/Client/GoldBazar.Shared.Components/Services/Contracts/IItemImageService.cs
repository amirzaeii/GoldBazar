using Microsoft.AspNetCore.Components.Forms;

namespace GoldBazar.Shared.Components.Services.Contracts;

public interface IItemImageService
{
    Task<string> UploadItemImage(IBrowserFile file);
    string GetProductImageUrl(ItemDTO item)
        => GetProductImageUrl(item.Id);

    string GetProductImageUrl(int productId);
    string GetTypeImageUrl(ItemCategoryDTO type)
        => GetTypeImageUrl(type.Id);
    string GetTypeImageUrl(int typeId);

}
