namespace GoldBazar.Vendor.Web.Components.Pages;
public partial class Profile : ComponentBase
{
    [Inject]
    private ShopService _shopService { get; set; } = default!;
    [Inject]
    private IImageService _imageService { get; set; } = default!;
    [Parameter]
    public int shopId { get; set; }
    private ShopDTO shop { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        shop = await _shopService.GetShopById(shopId);
        await base.OnInitializedAsync();
    }
}