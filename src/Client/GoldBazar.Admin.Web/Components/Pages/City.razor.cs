using System.Linq;

using GoldBazar.Admin.Web.Components.Dialogs;
using GoldBazar.Shared.DTOs;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Pages;

public partial class City
{
    private CityDTO[] cities = default!;
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
        => await LoadCities();

    private async Task LoadCities()
    {
        try
        {
            _loading = true;
            cities = await regionService.GetCities();
        }
        catch
        {
            Snackbar.Add("Failed to load Cities", Severity.Error);
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task ModifyCityDialog(CityDTO city)
    {
        var parameters = new DialogParameters
        {
            {"City" , city},
            {"Title" , city.Id == 0 ? "Create New City" : $"Edit City: {city.Name}" },
            {"OnSave", new Func<CityDTO, Task>(OnModifyCity) }
        };
        var dialog = await DialogService.ShowAsync<ModifyCity>("Modify City", parameters);
    }

    private async Task DeleteDialog(int cityId)
    {
        var parameters = new DialogParameters
        {
            {"Id" , cityId },
            {"Message" , "Are you sure you want to delete this city?" },
            {"OnDelete", new Func<int, Task>(OnDeleteCity) }
        };
        var dialog = await DialogService.ShowAsync<DeleteConfirmation>(
            "Confirm Deletion",
            parameters
        );
    }

    public async Task OnModifyCity(CityDTO city)
    {
        CityDTO? result;

        if (city.Id == 0)
        {

            result = await regionService.AddCity(city);
            if (result is not null && result.Id > 0)
            {

                cities = cities.Append(result).ToArray();
                Snackbar.Add("City created successfully", Severity.Success);
            }
            else
            {
                Snackbar.Add("Failed to create city", Severity.Error);
            }
        }
        else
        {

            result = await regionService.UpdateCity(city);
            if (result is not null)
            {

                cities = cities
                    .Select(c => c.Id == result.Id ? result : c)
                    .ToArray();
                Snackbar.Add("City updated successfully", Severity.Success);
            }
            else
            {
                Snackbar.Add("Failed to update city", Severity.Error);
            }
        }

        StateHasChanged();
    }

    public async Task OnDeleteCity(int id)
    {
        var result = await regionService.DeleteCity(id);
        if (result)
        {
            cities = cities.Where(w => w.Id != id).ToArray();
            Snackbar.Add("City deleted successfully", Severity.Success);
        }
        else
        {
            Snackbar.Add("City deleted failed", Severity.Error);
        }
        StateHasChanged();
    }

}
