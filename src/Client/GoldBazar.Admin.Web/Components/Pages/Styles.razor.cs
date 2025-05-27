using GoldBazar.Admin.Web.Components.Dialogs;
using GoldBazar.Shared.DTOs;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Pages;

public partial class Styles
{
    private StyleDTO[] styles = default!;
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
        => await LoadStyles();

    private async Task LoadStyles()
    {
        try
        {
            _loading = true;
            styles = await CatalogService.GetStyles();
        }
        catch
        {
            Snackbar.Add("Failed to load styles", Severity.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task ModifyStyleDialog(StyleDTO style)
    {
        var parameters = new DialogParameters
        {
            {"Style" , style },
            {"Title" , style.Id == 0 ? "Create New Style" : $"Edit Style: {style.Name}" },
            {"OnSave", new Func<StyleDTO, Task>(OnModifyStyle) }
        };
        var dialog = await DialogService.ShowAsync<ModifyStyle>("Modify Style", parameters);
    }

    private async Task DeleteDialog(int styleId)
    {
        var parameters = new DialogParameters
        {
            {"Id" , styleId },
            {"Message" , "Are you sure you want to delete this style?" },
            {"OnDelete", new Func<int, Task>(OnDeleteStyle) }
        };
        var dialog = await DialogService.ShowAsync<DeleteConfirmation>(
            "Confirm Deletion",
            parameters
        );
    }

    public async Task OnModifyStyle(StyleDTO style)
    {
        var result = new StyleDTO();
        if (style.Id == 0)
        {
            result = await CatalogService.AddStyle(style);
            if (result?.Id > 0) styles = styles.Append(result).ToArray();
            Snackbar.Add("Style updated successfully", Severity.Success);
        }
        else
        {
            await CatalogService.EditStyle(style);
            Snackbar.Add("Style created successfully", Severity.Success);
        }
        StateHasChanged();
    }
    public async Task OnDeleteStyle(int id)
    {
        var result = await CatalogService.DeleteStyle(id);
        if (result)
        {
            styles = styles.Where(w => w.Id != id).ToArray();
            Snackbar.Add("Style deleted successfully", Severity.Success);
        }
        else
        {
            Snackbar.Add("Style deleted failed", Severity.Error);
        }
        StateHasChanged();
    }
}
