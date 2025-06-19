using GoldBazar.Shared.DTOs;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Dialogs;

public partial class ModifyShop
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; }
    [Parameter] public string Title { get; set; }
    [Parameter] public ShopDTO Shop { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<ShopDTO, Task> OnSave { get; set; } = _ => Task.CompletedTask;

    private MudForm form;
    private CityDTO[] _cities = Array.Empty<CityDTO>();
    private MudDatePicker _picker;
    private DateTime? _joinDate = DateTime.Today;
    private GovernateDTO[] _governorates = Array.Empty<GovernateDTO>();
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
            _picker?.GoToDate(DateTime.Today);
    }

    private List<IBrowserFile> logoFiles = new();
    private List<IBrowserFile> bannerFiles = new();
    private int _selectedGovernorateId;
    private string? logoPreview;
    private string? bannerPreview;
    private async Task OnGovernorateChanged(int governorateId)
    {
        _cities = await regionService.GetCitiesByGovernorate(governorateId);
        Shop.CityId = 0; 
        StateHasChanged();
    }
    private int SelectedGovernorateId
    {
        get => _selectedGovernorateId;
        set
        {
            if (_selectedGovernorateId != value)
            {
                _selectedGovernorateId = value;
                _ = OnGovernorateChanged(value);
            }
        }
    }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            _governorates = await regionService.GetGovernorates();

            // Load cities if editing
            if (Shop.CityId > 0)
            {
                var selectedCity = await regionService.GetCityById(Shop.CityId);
                SelectedGovernorateId = selectedCity.GovernorateId;
                _cities = await regionService.GetCitiesByGovernorate(SelectedGovernorateId);
            }

            _joinDate = Shop.JoinDate.DateTime;
        }
        catch
        {
            Snackbar.Add("Failed to load cities", Severity.Error);
        }
    }

    void Cancel() => MudDialog.Cancel();

    private async Task OnLogoFilesChanged(InputFileChangeEventArgs e)
    {
        var file = e.File;
        var (dataUrl, bytes) = await ResizeImage(file);
        logoFiles.Clear(); logoFiles.Add(file);
        logoPreview = dataUrl;
        // TODO: upload bytes and set Shop.LogoUrl
    }

    private async Task OnBannerFilesChanged(InputFileChangeEventArgs e)
    {
        var file = e.File;
        var (dataUrl, bytes) = await ResizeImage(file);
        bannerFiles.Clear(); bannerFiles.Add(file);
        bannerPreview = dataUrl;
        // TODO: upload bytes and set Shop.BannerUrl
    }

    private static async Task<(string, byte[])> ResizeImage(IBrowserFile file)
    {
        var resized = await file.RequestImageFileAsync(file.ContentType, 500, 600);
        await using var ms = new MemoryStream();
        await resized.OpenReadStream(50_000_000).CopyToAsync(ms);
        var bytes = ms.ToArray();
        var url = $"data:{file.ContentType};base64,{Convert.ToBase64String(bytes)}";
        return (url, bytes);
    }

    async Task Submit()
    {
        await form.Validate();
        if (!form.IsValid) return;
        if (Shop.Id == 0)
            _joinDate = DateTime.Today;
        else
            _joinDate = Shop.JoinDate.DateTime;
        MudDialog.Close(DialogResult.Ok(true));
        try { await OnSave(Shop); }
        catch (Exception ex)
        {
            Snackbar.Add($"Error saving shop: {ex.Message}", Severity.Error);
        }
    }
}
