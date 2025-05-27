using GoldBazar.Admin.Web.Components.Dialogs;
using GoldBazar.Shared.DTOs;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Pages;

public partial class Governate
{
    private GovernateDTO[] govs = default!;
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
        => await LoadGovernorate();

    private async Task LoadGovernorate()
    {
        try
        {
            _loading = true;
            govs = await regionService.GetGovernorates();
        }
        catch
        {
            Snackbar.Add("Failed to load Governorates", Severity.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task ModifyGovernorateDialog(GovernateDTO gov)
    {
        var parameters = new DialogParameters
        {
            {"Governorate" , gov },
            {"Title" , gov.Id == 0 ? "Create New Governorate" : $"Edit Governorate: {gov.Name}" },
            {"OnSave", new Func<GovernateDTO, Task>(OnModifyGovernorate) }
        };
        var dialog = await DialogService.ShowAsync<ModifyGov>("Modify Governorate", parameters);
    }

    private async Task DeleteDialog(int govId)
    {
        var parameters = new DialogParameters
        {
            {"Id" , govId },
            {"Message" , "Are you sure you want to delete this Governorate?" },
            {"OnDelete", new Func<int, Task>(OnDeleteGovernorate) }
        };
        var dialog = await DialogService.ShowAsync<DeleteConfirmation>(
            "Confirm Deletion",
            parameters
        );
    }

    public async Task OnModifyGovernorate(GovernateDTO gov)
    {
        var result = new GovernateDTO();
        if (gov.Id == 0)
        {
            result = await regionService.AddGovernorate(gov);
            if (result?.Id > 0) govs = govs.Append(result).ToArray();
            Snackbar.Add("Governorate updated successfully", Severity.Success);
        }
        else
        {
            await regionService.UpdateGovernorate(gov);
            Snackbar.Add("Governorate edited successfully", Severity.Success);
        }
        StateHasChanged();
    }
    public async Task OnDeleteGovernorate(int id)
    {
        var result = await regionService.DeleteGovernorate(id);
        if (result)
        {
            govs = govs.Where(w => w.Id != id).ToArray();
            Snackbar.Add("Governorate deleted successfully", Severity.Success);
        }
        else
        {
            Snackbar.Add("Governorate deleted failed", Severity.Error);
        }
        StateHasChanged();
    }
}
