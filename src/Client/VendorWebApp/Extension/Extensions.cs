

using WebComponent.Services;

namespace VendorWebApp.Extension;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        //  builder.AddAuthenticationServices();

        builder.AddRabbitMqEventBus("EventBus")
               .AddEventBusSubscriptions();

        builder.Services.AddHttpForwarderWithServiceDiscovery();

        // Application services
        //builder.Services.AddScoped<BasketState>();
        //builder.Services.AddScoped<LogOutService>();
        //builder.Services.AddSingleton<BasketService>();
        //builder.Services.AddSingleton<OrderStatusNotificationService>();
        builder.Services.AddSingleton<IProductImageUrlProvider, ProductImageUrlProvider>();
        //builder.AddAIServices();

        // HTTP and GRPC client registrations
        //builder.Services.AddGrpcClient<Basket.BasketClient>(o => o.Address = new("http://basket-api"))
        //  .AddAuthToken();

        builder.Services.AddHttpClient<ICatalogService, CatalogService>(o => o.BaseAddress = new("http://catalog-api"))
            .AddApiVersion(1.0);
        //.AddAuthToken();

        builder.Services.AddHttpClient<IFilterService, FilterService>(o => o.BaseAddress = new("http://catalog-api"))
            .AddApiVersion(1.0);
        //.AddAuthToken();



        builder.Services.AddHttpClient<IShopService, ShopService>(o => o.BaseAddress = new("http://catalog-api"))
            .AddApiVersion(1.0);

        // builder.Services.AddHttpClient<OrderingService>(o => o.BaseAddress = new("http://ordering-api"))
        //     .AddApiVersion(1.0)
        //     .AddAuthToken();
    }

    public static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
    {
        // eventBus.AddSubscription<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
        // eventBus.AddSubscription<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();
        // eventBus.AddSubscription<OrderStatusChangedToStockConfirmedIntegrationEvent, OrderStatusChangedToStockConfirmedIntegrationEventHandler>();
        // eventBus.AddSubscription<OrderStatusChangedToShippedIntegrationEvent, OrderStatusChangedToShippedIntegrationEventHandler>();
        // eventBus.AddSubscription<OrderStatusChangedToCancelledIntegrationEvent, OrderStatusChangedToCancelledIntegrationEventHandler>();
        // eventBus.AddSubscription<OrderStatusChangedToSubmittedIntegrationEvent, OrderStatusChangedToSubmittedIntegrationEventHandler>();
    }
}
