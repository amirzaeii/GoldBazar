using WebComponent.Services;

public static class Extensions
{
    public static void AddApplicationServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IProductImageUrlProvider, ProductImageUrlProvider>();
        builder.Services.AddHttpClient<ICatalogService, CatalogService>(o => o.BaseAddress = new("https://localhost:7249"))
                    .AddApiVersion(1.0);


        builder.Services.AddHttpClient<IShopService, ShopService>(o => o.BaseAddress = new("https://localhost:7249")).AddApiVersion(1.0);


        // builder.Services.AddHttpClient<OrderingService>(o => o.BaseAddress = new("http://ordering-api"))
        //     .AddApiVersion(1.0)
        //     .AddAuthToken();

       
    }
}