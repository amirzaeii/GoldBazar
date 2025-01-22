using System.Reflection;
using System.Runtime.Loader;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.AddApplicationServices();
// server interactive
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var app = builder.Build();

app.MapDefaultEndpoints();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// default server interactive
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(AssemblyLoadContext.Default.Assemblies.Where(asm => asm.GetName().Name?.Contains("WebComponent") is true).Except([Assembly.GetExecutingAssembly()]).ToArray());

app.MapForwarder("/product-images/{id}", "http://catalog-api", "/api/catalog/items/{id}/pic");
app.MapForwarder("/type-images/{id}", "http://catalog-api", "/api/catalog/types/{id}/pic");


app.Run();
