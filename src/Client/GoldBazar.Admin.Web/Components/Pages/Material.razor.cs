using GoldBazar.Admin.Web.Components.Dialogs;
using GoldBazar.Shared.DTOs;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Pages;

public partial class Material
{
    private MaterialDTO[] materials = default!;
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
        => await LoadMaterials();

    private async Task LoadMaterials()
    {
        try
        {
            _loading = true;
            materials = await CatalogService.GetMaterials();
        }
        catch
        {
            Snackbar.Add("Failed to load Materials", Severity.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task ModifyMaterialDialog(MaterialDTO material)
    {
        var parameters = new DialogParameters
        {
            {"Material" , material},
            {"Title" , material.Id == 0 ? "Create New Material" : $"Edit Material: {material.Name}" },
            {"OnSave", new Func<MaterialDTO, Task>(OnModifyMaterial) }
        };
        var dialog = await DialogService.ShowAsync<ModifyMaterial>("Modify Material", parameters);
    }

    private async Task DeleteDialog(int materialId)
    {
        var parameters = new DialogParameters
        {
            {"Id" , materialId },
            {"Message" , "Are you sure you want to delete this material?" },
            {"OnDelete", new Func<int, Task>(OnDeleteMaterial) }
        };
        var dialog = await DialogService.ShowAsync<DeleteConfirmation>(
            "Confirm Deletion",
            parameters
        );
    }

    public async Task OnModifyMaterial(MaterialDTO material)
    {
        var result = new MaterialDTO();
        if (material.Id == 0)
        {
            result = await CatalogService.AddMaterial(material);
            if (result?.Id > 0) materials = materials.Append(result).ToArray();
            Snackbar.Add("Material updated successfully", Severity.Success);
        }
        else
        {
            await CatalogService.UpdateMaterial(material);
            Snackbar.Add("Material created successfully", Severity.Success);
        }
        StateHasChanged();
    }
    public async Task OnDeleteMaterial(int id)
    {
        var result = await CatalogService.DeleteMaterial(id);
        if (result)
        {
            materials = materials.Where(w => w.Id != id).ToArray();
            Snackbar.Add("Material deleted successfully", Severity.Success);
        }
        else
        {
            Snackbar.Add("Material deleted failed", Severity.Error);
        }
        StateHasChanged();
    }
}
