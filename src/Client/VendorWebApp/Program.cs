using VendorWebApp.Components;
using VendorWebApp.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
//localization
builder.Services.AddLocalization();

builder.AddApplicationServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();
string[] supportedCultures = ["en-US", "ar-IQ", "ku-Arab"];
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapGet("Culture/Set", async (HttpContext context) =>
{
    try
    {
        var culture = context.Request.Query["culture"].ToString();
        var redirectUri = context.Request.Query["redirectUri"].ToString();

        if (!string.IsNullOrEmpty(culture))
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true
            };
            context.Response.Cookies.Append(".AspNetCore.Culture", $"c={culture}|uic={culture}", cookieOptions);
        }

        if (!string.IsNullOrEmpty(redirectUri))
        {
            context.Response.Redirect(redirectUri);
        }
        else
        {
            context.Response.Redirect("/");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error setting culture: {ex.Message}");
        context.Response.StatusCode = 500;
    }
});

app.Run();
