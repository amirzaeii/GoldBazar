using System.Text.Json;
using Microsoft.AspNetCore.Components;

using WebComponent.Commons.Consts;
using WebComponent.Dtos;
using WebComponent.Services;

namespace WebComponent.Components.Pages.Categories;

public partial class Categories
{
    [Inject] 
    protected CatalogService CatalogService { get; set; }
    [Inject]
    protected LocalStorageService LocalStorageService { get; set; }
    [Parameter] 
    public int? ItemTypeId { get; set; }

    protected List<CatalogItem> Products { get; set; } = new();

    protected CompositeFilterDto CompositeFilter { get; set; } = new();

    private string _selectedValue = "string.Empty";

    protected string SelectedValue
    {
        get { return _selectedValue; }
        set
        {
            if (_selectedValue != value)
            {
                _selectedValue = value;
                InvokeAsync(async () => await FilterProducts());
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && ItemTypeId is null)
        {
            await LoadFilter();
            StateHasChanged();
        }
    }
    
    protected override async Task OnInitializedAsync()
    {
        if (ItemTypeId is not null)
        {
            CompositeFilter = new CompositeFilterDto
            {
                ProductTypes = [ItemTypeId.Value]
            };
        }
        await FilterProducts();
    }

    protected async Task FilterProducts()
    {
        IEnumerable<CatalogItem> result = await CatalogService.FilterByComposite(CompositeFilter);
        switch (SelectedValue)
        {
            case "PriceHighToLow":
                Products = result.OrderByDescending(x => x.Price).ToList();
                break;
            case "PriceLowToHigh":
                Products = result.OrderBy(x => x.Price).ToList();
                break;
            default:
                Products = result.ToList();
                break;
        }
    }

    private async Task LoadFilter()
    {
        var filterJson = await LocalStorageService.GetItemAsync(LocalStorageKeys.CompositeFilter);
        if (!string.IsNullOrEmpty(filterJson))
        {
            CompositeFilter = JsonSerializer.Deserialize<CompositeFilterDto>(filterJson) ?? new CompositeFilterDto();
        }
    }
    
    private void SetSortBy(string? sortBy)
    {
        _selectedValue = sortBy ?? string.Empty;
        InvokeAsync(async () => await FilterProducts());
    }
}