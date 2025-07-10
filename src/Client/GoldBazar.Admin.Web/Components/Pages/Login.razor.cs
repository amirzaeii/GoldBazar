using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GoldBazar.Admin.Web.Components.Pages;

public partial class Login : ComponentBase
{
    protected MudForm form = default!;
    protected bool _loading;
    protected string _errorMessage = string.Empty;
    protected LoginModel loginModel = new();

    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] ISnackbar Snackbar { get; set; } = default!;

    private async Task HandleValidSubmit()
    {
    }

    private void ForgotPassword()
    {
        Snackbar.Add("Forgot password clicked – implement your flow here.", Severity.Info);
    }

    public class LoginModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}