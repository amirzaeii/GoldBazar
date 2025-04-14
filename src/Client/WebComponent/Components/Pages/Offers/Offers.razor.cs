using Microsoft.AspNetCore.Components;

using WebComponent.Dtos;
using WebComponent.Services;

namespace WebComponent.Components.Pages.Offers;

public partial class Offers
{
    const int PageSize = 10;

    [Inject] 
    protected CatalogService CatalogService { get; set; }

    protected int CurrentPage { get; set; } = 1;

    protected CatalogResult _catalogResult = new(0, 0, 0, new List<CatalogItem>());
    private ICollection<CatalogItem> _discountedCatalogItems { get; set; } = new List<CatalogItem>();
    protected bool IsLoaded { get; set; }

    static int GetVisiblePageIndexes(CatalogResult result)
        => (int)Math.Ceiling(1.0 * result.Count / PageSize);

    protected override async Task OnInitializedAsync()
    {
        await GetCatalogItems();
    }

    protected async Task GetCatalogItems()
    {
        IsLoaded = false;
        IEnumerable<CatalogItem> result = await CatalogService.GetDiscountedCatalogItems(
            CurrentPage - 1,
            PageSize);
        _discountedCatalogItems = result.ToHashSet();
        IsLoaded = true;
    }

    private async Task PageChanged(int page)
    {
        if (CurrentPage == page)
        {
            return;
        }
        CurrentPage = page;
        await GetCatalogItems();
    }
}