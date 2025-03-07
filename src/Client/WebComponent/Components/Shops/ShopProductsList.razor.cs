using Microsoft.AspNetCore.Components;

using WebComponent.Dtos;

namespace WebComponent.Components.Shops;

public partial class ShopProductsList
{
    [Parameter]
    public int ShopId { get; set; }
    [Parameter]
    public int ExcludeProductId { get; set; }

    List<CatalogItem>? _shopItems;
    string _goldsmithName = "";

    protected override async Task OnInitializedAsync()
    {
        // Fetch shop name
        var shop = await ShopService.GetShopById(ShopId);
        _goldsmithName = shop?.Name ?? "Goldsmith";

        // Fetch products from the shop, excluding the current product
        _shopItems = (await ShopService.GetProductsByShopId(ShopId))
            .Where(p => p.Id != ExcludeProductId)
            .ToList();
    }
}