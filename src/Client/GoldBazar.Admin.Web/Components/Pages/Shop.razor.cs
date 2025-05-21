using GoldBazar.Admin.Web.Components.Dialogs;
using GoldBazar.Shared.DTOs;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Pages;

public partial class Shop
{
    private ShopDTO[] shops = default!;
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
        => await LoadShops();

    private async Task LoadShops()
    {
        try
        {
            _loading = true;
            shops = await ShopService.GetAllShops();
        }
        catch
        {
            Snackbar.Add("Failed to load Shops", Severity.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task ModifyShopDialog(ShopDTO shop)
    {
        var parameters = new DialogParameters
        {
            {"Shop" , shop},
            {"Title" , shop.Id == 0 ? "Create New Shop" : $"Edit Shop: {shop.Name}" },
            {"OnSave", new Func<ShopDTO, Task>(OnModifyShop) }
        };
        var dialog = await DialogService.ShowAsync<ModifyShop>("Modify Shop", parameters);
    }

    private async Task DeleteDialog(int shopId)
    {
        var parameters = new DialogParameters
        {
            {"Id" , shopId },
            {"Message" , "Are you sure you want to delete this shop?" },
            {"OnDelete", new Func<int, Task>(OnDeleteShop) }
        };
        var dialog = await DialogService.ShowAsync<DeleteConfirmation>(
            "Confirm Deletion",
            parameters
        );
    }

    public async Task OnModifyShop(ShopDTO shop)
    {
        var result = new ShopDTO();
        if (shop.Id == 0)
        {
            result = await ShopService.AddShop(shop);
            if (result?.Id > 0) shops = shops.Append(result).ToArray();
            Snackbar.Add("shop updated successfully", Severity.Success);
        }
        else
        {
            await ShopService.UpdateShop(shop.Id, shop);
            Snackbar.Add("Shop created successfully", Severity.Success);
        }
        StateHasChanged();
    }
    public async Task OnDeleteShop(int id)
    {
        var result = await ShopService.DeleteShop(id);
        if (result)
        {
            shops = shops.Where(w => w.Id != id).ToArray();
            Snackbar.Add("Shop deleted successfully", Severity.Success);
        }
        else
        {
            Snackbar.Add("Shop deleted failed", Severity.Error);
        }
        StateHasChanged();
    }
}
