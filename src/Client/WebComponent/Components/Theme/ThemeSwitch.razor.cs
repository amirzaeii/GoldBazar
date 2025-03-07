using Microsoft.AspNetCore.Components;

using WebComponent.Commons.Consts;
using WebComponent.Services;

namespace WebComponent.Components.Theme;

public partial class ThemeSwitch
{
    [Inject] 
    public LocalStorageService LocalStorageService { get; set; }
    protected bool _isDarkMode;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadTheme();
            StateHasChanged();
        }
    }

    private async Task SaveTheme()
    {
        await LocalStorageService.SetItemAsync(LocalStorageKeys.Theme, _isDarkMode ? "dark" : "light");
    }

    private async Task LoadTheme()
    {
        string theme = await LocalStorageService.GetItemAsync(LocalStorageKeys.Theme);
        if (!string.IsNullOrEmpty(theme))
        {
            _isDarkMode = theme == "dark";
        }
    }

    private async Task ChangeTheme(bool isDarkMode)
    {
        _isDarkMode = isDarkMode;
        await SaveTheme();
    }
}