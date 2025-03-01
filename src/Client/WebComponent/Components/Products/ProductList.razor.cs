using Microsoft.AspNetCore.Components;

using WebComponent.Dtos;

namespace WebComponent.Components.Products;

public partial class ProductList
{
    const int PageSize = 10;
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Parameter] 
    public bool ShowPagination { get; set; }
    [Parameter]
    public CompositeFilterDto CompositeFilter { get; set; } = new();

    [SupplyParameterFromQuery(Name = "type")]
    public int? ItemTypeId { get; set; }

    protected int CurrentPage { get; set; } = 1;

    protected CatalogResult _catalogResult = new(0,0,0,new List<CatalogItem>());

    static int GetVisiblePageIndexes(CatalogResult result)
        => (int)Math.Ceiling(1.0 * result.Count / PageSize);
    protected override async Task OnInitializedAsync()
    {
        await GetCatalogItems();
    }

    protected async Task GetCatalogItems()
    {
        _catalogResult = await CatalogService.GetCatalogItems(
            CurrentPage - 1,
            PageSize,
            ItemTypeId);
    }

    private async Task PageChanged(int page)
    {
        CurrentPage = page;
        await GetCatalogItems();
    }
}