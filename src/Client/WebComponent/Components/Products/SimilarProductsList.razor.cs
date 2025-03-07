using Microsoft.AspNetCore.Components;

using WebComponent.Dtos;

namespace WebComponent.Components.Products;

public partial class SimilarProductsList
{
    [Parameter]
    public int TypeId { get; set; }

    private IEnumerable<CatalogItem> _similarProducts = [];

    protected override async Task OnInitializedAsync()
    {
        _similarProducts = await CatalogService.GetSimilarProducts(TypeId);
    }
}