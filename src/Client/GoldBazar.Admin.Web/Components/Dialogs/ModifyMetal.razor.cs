using GoldBazar.Shared.DTOs;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Dialogs;

public partial class ModifyMetal
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = default!;
    private MudForm form = default!;
    [Parameter] public string Title { get; set; } = default!;
    [Parameter] public MetalDTO Metal { get; set; } = default!;
    [Parameter][EditorRequired] public Func<MetalDTO, Task>? OnSave { get; set; }

    private List<MaterialDTO> Materials = new();
    private List<MetalDTO> ExistingMetals = new();
    private string ErrorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        Materials = (await StyleService.GetMaterials()).ToList();
        ExistingMetals = (await StyleService.GetMetals()).ToList();
    }

    void Cancel() => MudDialog.Cancel();

    async Task Submit()
    {
        ErrorMessage = string.Empty;
        await form.Validate();
        if (!form.IsValid) return;

        var exists = ExistingMetals.Any(m =>
            m.Name.Equals(Metal.Name.Trim(), StringComparison.OrdinalIgnoreCase)
            && m.MaterialId == Metal.MaterialId
            && m.Id != Metal.Id);

        if (exists)
        {
            ErrorMessage = $"The metal '{Metal.Name}' already exists for the selected material.(Change the Material)";
            return;
        }

        try
        {
            if (OnSave != null)
            {
                MudDialog.Close(DialogResult.Ok(true));
                await OnSave.Invoke(Metal);
            }
        }
        catch
        {
            Snackbar.Add("An unexpected error occurred while saving.", Severity.Error);
        }
    }

    private void OnMaterialSelected(int selectedId)
    {
        Metal.MaterialId = selectedId;
        var selectedMaterial = Materials.FirstOrDefault(m => m.Id == selectedId);
        Metal.MaterialName = selectedMaterial?.Name ?? string.Empty;
    }
}
