using GoldBazar.Shared.DTOs;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Dialogs;

public partial class ModifyOccasion
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = default!;
    private MudForm form = default!;
    [Parameter] public string Title { get; set; } = default!;
    [Parameter] public OccasionDTO Occasion { get; set; } = default!;
    [Parameter][EditorRequired] public Func<OccasionDTO, Task>? OnSave { get; set; }
    void Cancel() => MudDialog.Cancel();

    async Task Submit()
    {
        await form.Validate();
        if (!form.IsValid) return;

        try
        {
            if (OnSave != null)
            {
                MudDialog.Close(DialogResult.Ok(true));
                await OnSave.Invoke(Occasion);
            }
        }
        catch
        {
            Snackbar.Add("Entered Occasion already exists", Severity.Error);
        }
    }

}
