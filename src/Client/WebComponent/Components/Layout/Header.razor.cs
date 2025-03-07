using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using WebComponent.Constants;

namespace WebComponent.Components.Layout;

public partial class Header
{
    [Inject]
    protected NavigationManager NavigationManager { get; set; }
    
     protected void NavigateToHome()
    {
        NavigationManager.NavigateTo(@RouteConstants.HOME);
    }
}