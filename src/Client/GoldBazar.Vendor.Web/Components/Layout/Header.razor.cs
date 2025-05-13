namespace GoldBazar.Vendor.Web.Components.Layout;
public partial class Header : ComponentBase
{
    [Inject]
    private IImageService _imageService { get; set; } = default!;
    [Parameter]
    public int shopId { get; set; } = 1;
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
}
