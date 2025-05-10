using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static void AddShareApplicationServices(this IHostApplicationBuilder builder)
    {
        // Application services
        builder.Services.AddSingleton<IProductImageUrlProvider, ProductImageUrlProvider>();
        builder.Services.AddHttpClient<CatalogService>(o => o.BaseAddress = new("http://catalog-api"))
            .AddApiVersion(1.0);
        //.AddAuthToken();

        builder.Services.AddHttpClient<OrderingService>(o => o.BaseAddress = new("http://ordering-api"))
            .AddApiVersion(1.0);
        //     .AddAuthToken();
    }

    public static async Task<string?> GetBuyerIdAsync(this AuthenticationStateProvider authenticationStateProvider)
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        //return user.FindFirst("sub")?.Value;
        return "1ef50770-dc75-4edd-b658-b8137cfa3770";
    }

    public static async Task<string?> GetUserNameAsync(this AuthenticationStateProvider authenticationStateProvider)
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        //return user.FindFirst("name")?.Value;
        return "abbas";
    }
}
