using GoldBazar.Shared.DTOs;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Dialogs;

public partial class ModifyCity
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = default!;
    [Parameter] public string Title { get; set; } = default!;
    [Parameter] public CityDTO City { get; set; } = new CityDTO();
    [Parameter][EditorRequired] public Func<CityDTO, Task> OnSave { get; set; } = _ => Task.CompletedTask;

    private MudForm form = default!;
    private GovernateDTO[] _governorates = Array.Empty<GovernateDTO>();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            // load all governorates for the dropdown
            _governorates = await regionService.GetGovernorates();

            // if editing existing city, ensure City.GovernorateName is set
            if (City.GovernorateId != 0)
            {
                var gov = _governorates.FirstOrDefault(g => g.Id == City.GovernorateId);
                if (gov is not null)
                    City.GovernorateName = gov.Name;
            }
        }
        catch
        {
            Snackbar.Add("Failed to load governorates", Severity.Error);
        }
    }

    void Cancel() => MudDialog.Cancel();

    async Task Submit()
    {
        await form.Validate();
        if (!form.IsValid)
            return;

        // sync the selected governorate name back into the DTO
        var selected = _governorates.FirstOrDefault(g => g.Id == City.GovernorateId);
        City.GovernorateName = selected?.Name ?? string.Empty;

        MudDialog.Close(DialogResult.Ok(true));
        try
        {
            await OnSave(City);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Error saving city: {ex.Message}", Severity.Error);
        }
    }
}
