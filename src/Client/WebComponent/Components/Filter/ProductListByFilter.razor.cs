using Microsoft.AspNetCore.Components;

using WebComponent.Dtos;

namespace WebComponent.Components.Filter;

public partial class ProductListByFilter
{
    [Parameter]
    public List<CatalogItem> Products { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        // Parse the query string from the current URI
        var uri = Navigation.ToAbsoluteUri(Navigation.Uri);
        var queryParameters = System.Web.HttpUtility.ParseQueryString(uri.Query);

        // Create a CompositeFilterDto from the query parameters
        var filters = new Dtos.CompositeFilterDto
        {
            MinWeight = decimal.TryParse(queryParameters["minWeight"], out var minWeight) ? minWeight : 0,
            MaxWeight = decimal.TryParse(queryParameters["maxWeight"], out var maxWeight) ? maxWeight : 0,
            Metals = queryParameters["metals"]?.Split(',').Select(int.Parse).ToList() ?? new List<int>(),
            Styles = queryParameters["styles"]?.Split(',').Select(int.Parse).ToList() ?? new List<int>(),
            Occasions = queryParameters["occasions"]?.Split(',').Select(int.Parse).ToList() ?? new List<int>(),
            Materials = queryParameters["materials"]?.Split(',').Select(int.Parse).ToList() ?? new List<int>(),
        };

        // Fetch the filtered products
        Products = (await CatalogService.FilterByComposite(filters)).ToList();
    }
}