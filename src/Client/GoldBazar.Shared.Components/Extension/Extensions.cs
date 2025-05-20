using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Hosting;
namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static void AddShareApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHttpForwarderWithServiceDiscovery();

        // Application services
        builder.Services.AddHttpClient<IImageService, ImageService>(o => o.BaseAddress = new("http://catalog-api"))
         .AddApiVersion(1.0);

        builder.Services.AddHttpClient<CatalogService>(o => o.BaseAddress = new("http://catalog-api"))
            .AddApiVersion(1.0);
        //.AddAuthToken();

          builder.Services.AddHttpClient<ShopService>(o => o.BaseAddress = new("http://catalog-api"))
            .AddApiVersion(1.0);
        //.AddAuthToken();

        builder.Services.AddHttpClient<OrderingService>(o => o.BaseAddress = new("http://ordering-api"))
            .AddApiVersion(1.0);
        //     .AddAuthToken();      

        builder.Services.AddHttpClient<RegionService>(o => o.BaseAddress = new("http://catalog-api"))
           .AddApiVersion(1.0);

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
