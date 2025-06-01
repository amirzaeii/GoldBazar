using GoldBazar.Shared.Components.Services;
using GoldBazar.Shared.DTOs;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Dialogs;

public partial class ModifyCategory
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; } = default!;
    private MudForm form = default!;

    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public CategoryDTO Category { get; set; } = new();
    [Parameter] public Func<CategoryDTO, Task>? OnSave { get; set; }

    [Inject] public CatalogService CatalogService { get; set; } = default!;

    void Cancel() => MudDialog.Cancel();

    private async Task Save()
    {
        await form.Validate();
        if (!form.IsValid) return;

        var result = Category.Id == 0
            ? await CatalogService.AddCategory(Category)
            : await CatalogService.EditCategory(Category);

        if (result is null)
        {
            Snackbar.Add("Save failed", Severity.Error);
            return;
        }

        MudDialog.Close(DialogResult.Ok(true));
        if (OnSave != null)
            await OnSave.Invoke(result);
    }
}


