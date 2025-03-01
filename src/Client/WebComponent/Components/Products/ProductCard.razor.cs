using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using WebComponent.Constants;
using WebComponent.Dtos;

namespace WebComponent.Components.Products;

public partial class ProductCard : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public CatalogItem Item { get; set; }
    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    protected string GetImage()
    {
        return ImageProvider.GetProductImageUrl(Item);
    }

    private void GoToProduct(int itemId)
    {
        NavigationManager.NavigateTo($"{RouteConstants.PRODUCTS}{itemId}");
    }
}