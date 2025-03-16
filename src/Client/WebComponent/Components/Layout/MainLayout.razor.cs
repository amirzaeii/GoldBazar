using System.Globalization;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using WebComponent.Commons.Consts;
using WebComponent.Services;

namespace WebComponent.Components.Layout;

public partial class MainLayout
{
    [Inject] 
    protected LocalStorageService LocalStorageService { get; set; }
    private bool IsRightToLeft { get; set; }
    private bool IsDarkMode { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadFilter();
            StateHasChanged();
        }
    }

    protected override void OnInitialized()
    {
        CultureInfo culture = CultureInfo.CurrentCulture;
        IsRightToLeft = culture.TextInfo.IsRightToLeft;
    }

    protected async Task LoadFilter()
    {
        string theme = await LocalStorageService.GetItemAsync(LocalStorageKeys.Theme);
        if (!string.IsNullOrEmpty(theme))
        {
            IsDarkMode = theme == "dark";
        }
    }
}