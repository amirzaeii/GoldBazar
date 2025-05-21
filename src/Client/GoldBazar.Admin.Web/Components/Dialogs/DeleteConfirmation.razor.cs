using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Dialogs;

public partial class DeleteConfirmation
{

    [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = default!;
    [Parameter] public int Id { get; set; }
    [Parameter] public string Message { get; set; } = default!;
    [Parameter][EditorRequired] public Func<int, Task>? OnDelete { get; set; }
    private void Cancel()
        => MudDialog.Cancel();

    async Task Submit()
    {
        if (OnDelete != null)
        {
            MudDialog.Close(DialogResult.Ok(true));
            await OnDelete.Invoke(Id);
        }
    }

}


