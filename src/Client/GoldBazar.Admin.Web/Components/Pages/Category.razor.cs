using GoldBazar.Admin.Web.Components.Dialogs;
using GoldBazar.Shared.DTOs;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Pages;

public partial class Category
{
    private CategoryDTO[] categories = Array.Empty<CategoryDTO>();
    private bool _loading;

    protected override async Task OnInitializedAsync() => await LoadCategories();

    private async Task LoadCategories()
    {
        _loading = true;
        try
        {
            categories = await CatalogService.GetCategories();
        }
        catch
        {
            Snackbar.Add("Failed to load categories", Severity.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    private string GetPhotoUrl(CategoryDTO cat)
    {
        if (string.IsNullOrWhiteSpace(cat.Photo))
            return "images/default-category.png";
        if (cat.Photo.StartsWith("http"))
            return cat.Photo;
        return $"api/catalog/categories/{cat.Id}/pic";
    }

    private async Task OpenModifyDialog(CategoryDTO category)
    {
        var parameters = new DialogParameters {
            { nameof(ModifyCategory.Category), category },
            { nameof(ModifyCategory.Title),
                category.Id == 0 ? "Create Category" : $"Edit: {category.Name}" },
            { nameof(ModifyCategory.OnSave),
                new Func<CategoryDTO, Task>(OnSaved) }
        };
        await DialogService.ShowAsync<ModifyCategory>(
            "Modify Category", parameters);
    }

    private async Task OnSaved(CategoryDTO updated)
    {
        if (updated.Id <= 0)
            return;

        categories = categories
            .Where(c => c.Id != updated.Id)
            .Append(updated)
            .ToArray();

        Snackbar.Add(
            categories.Any(c => c.Id == updated.Id && c == updated)
                ? "Saved successfully"
                : "Operation complete",
            Severity.Success);

        StateHasChanged();
    }

    private async Task OpenDeleteDialog(int id)
    {
        var parameters = new DialogParameters {
            { "Id", id },
            { "Message", "Delete this category?" },
            { "OnDelete", new Func<int, Task>(OnDeleted) }
        };
        await DialogService.ShowAsync<DeleteConfirmation>(
            "Confirm Delete", parameters);
    }

    private async Task OnDeleted(int id)
    {
        if (await CatalogService.DeleteCategory(id))
        {
            categories = categories.Where(c => c.Id != id).ToArray();
            Snackbar.Add("Deleted", Severity.Success);
        }
        else
        {
            Snackbar.Add("Delete failed", Severity.Error);
        }

        StateHasChanged();
    }
}

