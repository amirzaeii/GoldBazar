namespace GoldBazar.Shared.Components.Services;

public class ProductImageUrlProvider : IProductImageUrlProvider
{
    public string GetProductImageUrl(int productId)
        => $"product-images/{productId}?api-version=1.0";
    public string GetTypeImageUrl(int typeId)
        => $"type-images/{typeId}?api-version=1.0";
}