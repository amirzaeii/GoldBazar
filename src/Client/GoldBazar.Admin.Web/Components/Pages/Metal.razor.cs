using GoldBazar.Admin.Web.Components.Dialogs;
using GoldBazar.Shared.DTOs;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Pages;

public partial class Metal
{
    private MetalDTO[] metals = default!;
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
        => await LoadMetals();

    private async Task LoadMetals()
    {
        try
        {
            _loading = true;
            metals = await CatalogService.GetMetals();
        }
        catch
        {
            Snackbar.Add("Failed to load Metals", Severity.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task ModifyMetalDialog(MetalDTO metal)
    {
        var parameters = new DialogParameters
        {
            {"Metal" , metal},
            {"Title" , metal.Id == 0 ? "Create New Metal" : $"Edit Metal: {metal.Name}" },
            {"OnSave", new Func<MetalDTO, Task>(OnModifyMetal) }
        };
        var dialog = await DialogService.ShowAsync<ModifyMetal>("Modify Metal", parameters);
    }

    private async Task DeleteDialog(int metalId)
    {
        var parameters = new DialogParameters
        {
            {"Id" , metalId },
            {"Message" , "Are you sure you want to delete this Metal?" },
            {"OnDelete", new Func<int, Task>(OnDeleteMetal) }
        };
        var dialog = await DialogService.ShowAsync<DeleteConfirmation>(
            "Confirm Deletion",
            parameters
        );
    }

    public async Task OnModifyMetal(MetalDTO metal)
    {
        if (metal.Id == 0)
        {
            var result = await CatalogService.AddMetal(metal);
            if (result?.Id > 0)
            {
                metals = metals.Append(result).ToArray();
                Snackbar.Add("Metal created successfully", Severity.Success);
            }
        }
        else
        {
            var updated = await CatalogService.UpdateMetal(metal);
            if (updated != null)
            {
                metals = metals.Select(m => m.Id == updated.Id ? updated : m).ToArray();
                Snackbar.Add("Metal updated successfully", Severity.Success);
            }
        }

        StateHasChanged();
    }

    public async Task OnDeleteMetal(int id)
    {
        var result = await CatalogService.DeleteMetal(id);
        if (result)
        {
            metals = metals.Where(w => w.Id != id).ToArray();
            Snackbar.Add("Metal deleted successfully", Severity.Success);
        }
        else
        {
            Snackbar.Add("Metal deleted failed", Severity.Error);
        }
        StateHasChanged();
    }
}
