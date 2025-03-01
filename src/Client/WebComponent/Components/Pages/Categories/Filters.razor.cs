using System.Text.Json;

using Microsoft.AspNetCore.Components;

using WebComponent.Commons;
using WebComponent.Dtos;
using WebComponent.Services;

namespace WebComponent.Components.Pages.Categories;

public partial class Filters
{
    [Inject] 
    protected ICatalogService CatalogService { get; set; }
    [Inject] 
    public LocalStorageService LocalStorageService { get; set; }

    [Inject] 
    public NavigationManager NavigationManager { get; set; }
    protected CompositeFilterDto CompositeFilter { get; set; } = new();

    protected List<CatalogItem> Products { get; set; } = new();
    protected HashSet<CatalogItemType> CatalogTypes { get; set; } = new();
    protected HashSet<MaterialType> MaterialTypes { get; set; } = new();
    protected HashSet<MetalType> MetalTypes { get; set; } = new();
    protected HashSet<OccasionType> OccasionTypes { get; set; } = new();
    protected HashSet<StyleType> StyleTypes { get; set; } = new();
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadFilter();
            StateHasChanged();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        IEnumerable<CatalogItemType> result = await CatalogService.GetTypes();
        IEnumerable<MaterialType> materials = await CatalogService.GetMaterials();
        IEnumerable<MetalType> metals = await CatalogService.GetMetals();
        IEnumerable<OccasionType> occasions = await CatalogService.GetOccasions();
        IEnumerable<StyleType> styles = await CatalogService.GetStyles();
        CatalogTypes = result.ToHashSet();
        MaterialTypes = materials.ToHashSet();
        MetalTypes = metals.ToHashSet();
        OccasionTypes = occasions.ToHashSet();
        StyleTypes = styles.ToHashSet();
    }

    protected bool IsChecked(int id, FilterType type)
    {
        switch (type)
        {
            case FilterType.Metal:
                return CompositeFilter.Metals.Contains(id);
            case FilterType.Style:
                return CompositeFilter.Styles.Contains(id);
            case FilterType.Occasion:
                return CompositeFilter.Occasions.Contains(id);
            case FilterType.Material:
                return CompositeFilter.Materials.Contains(id);
            case FilterType.ProductType:
                return CompositeFilter.ProductTypes.Contains(id);
            default:
                throw new ArgumentException("Invalid filter type", nameof(type));
        }
    }

    protected void OnCheckboxChange(bool isChecked, int id, FilterType type)
    {
        switch (type)
        {
            case FilterType.Metal:
                UpdateFilterList(CompositeFilter.Metals, id, isChecked);
                break;
            case FilterType.Style:
                UpdateFilterList(CompositeFilter.Styles, id, isChecked);
                break;
            case FilterType.Occasion:
                UpdateFilterList(CompositeFilter.Occasions, id, isChecked);
                break;
            case FilterType.Material:
                UpdateFilterList(CompositeFilter.Materials, id, isChecked);
                break;
            case FilterType.ProductType:
                UpdateFilterList(CompositeFilter.ProductTypes, id, isChecked);
                break;
            default:
                throw new ArgumentException("Invalid filter type", nameof(type));
        }
    }

    private void UpdateFilterList(List<int> filterList, int id, bool isChecked)
    {
        if (isChecked)
        {
            if (!filterList.Contains(id))
            {
                filterList.Add(id);
            }
        }
        else
        {
            if (filterList.Contains(id))
            {
                filterList.Remove(id);
            }
        }
    }
    
    private void NavigateToCategories()
    {
        NavigationManager.NavigateTo("/categories");
    }
    
    protected void ClearAllFiltersAsync()
    {
        CompositeFilter = new CompositeFilterDto();
    }
    
    private async Task SaveFilter()
    {
        await LocalStorageService.SetItemAsync("compositeFilter", JsonSerializer.Serialize(CompositeFilter));
        NavigateToCategories();
    }
    
    private async Task LoadFilter()
    {
        var filterJson = await LocalStorageService.GetItemAsync("compositeFilter");
        if (!string.IsNullOrEmpty(filterJson))
        {
            CompositeFilter = JsonSerializer.Deserialize<CompositeFilterDto>(filterJson) ?? new CompositeFilterDto();
        }
    }
}