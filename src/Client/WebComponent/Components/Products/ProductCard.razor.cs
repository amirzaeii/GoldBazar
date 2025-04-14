using Microsoft.AspNetCore.Components;

using WebComponent.Constants;
using WebComponent.Dtos;

namespace WebComponent.Components.Products;

public partial class ProductCard : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public CatalogItem Item { get; set; } = default!;
    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    protected string GetImage()
    {
        return ImageProvider.GetProductImageUrl(Item);
    }
}