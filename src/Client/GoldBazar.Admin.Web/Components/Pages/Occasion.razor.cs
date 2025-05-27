using GoldBazar.Admin.Web.Components.Dialogs;
using GoldBazar.Shared.DTOs;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Pages;

public partial class Occasion
{
    private OccasionDTO[] occasions = default!;
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
        => await LoadOccasions();

    private async Task LoadOccasions()
    {
        try
        {
            _loading = true;
            occasions = await CatalogService.GetOccasions();
        }
        catch
        {
            Snackbar.Add("Failed to load Occasions", Severity.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task ModifyOccasionDialog(OccasionDTO occasion)
    {
        var parameters = new DialogParameters
        {
            {"Occasion" , occasion},
            {"Title" , occasion.Id == 0 ? "Create New Occasion" : $"Edit Occasion: {occasion.Name}" },
            {"OnSave", new Func<OccasionDTO, Task>(OnModifyOccasion) }
        };
        var dialog = await DialogService.ShowAsync<ModifyOccasion>("Modify Occasion", parameters);
    }

    private async Task DeleteDialog(int occasionId)
    {
        var parameters = new DialogParameters
        {
            {"Id" , occasionId },
            {"Message" , "Are you sure you want to delete this Occasion?" },
            {"OnDelete", new Func<int, Task>(OnDeleteOccasion) }
        };
        var dialog = await DialogService.ShowAsync<DeleteConfirmation>(
            "Confirm Deletion",
            parameters
        );
    }

    public async Task OnModifyOccasion(OccasionDTO occasion)
    {
        var result = new OccasionDTO();
        if (occasion.Id == 0)
        {
            result = await CatalogService.AddOccasion(occasion);
            if (result?.Id > 0) occasions = occasions.Append(result).ToArray();
            Snackbar.Add("Occasion updated successfully", Severity.Success);
        }
        else
        {
            await CatalogService.UpdateOccasion(occasion);
            Snackbar.Add("Occasion created successfully", Severity.Success);
        }
        StateHasChanged();
    }
    public async Task OnDeleteOccasion(int id)
    {
        var result = await CatalogService.DeleteOccasion(id);
        if (result)
        {
            occasions = occasions.Where(w => w.Id != id).ToArray();
            Snackbar.Add("Occasion deleted successfully", Severity.Success);
        }
        else
        {
            Snackbar.Add("Occasion deleted failed", Severity.Error);
        }
        StateHasChanged();
    }
}
