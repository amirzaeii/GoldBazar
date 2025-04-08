using Microsoft.AspNetCore.Components;

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
        try
        {
            Console.WriteLine($"Navigating to product with ID: {itemId}");
            NavigationManager.NavigateTo($"{RouteConstants.PRODUCTS}{itemId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Navigation failed: {ex.Message}");
        }
    }
}