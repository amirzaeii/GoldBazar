namespace GoldBazar.Admin.Web.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        //  builder.AddAuthenticationServices();

        builder.AddRabbitMqEventBus("gb-eventbus")
               .AddEventBusSubscriptions();

        builder.Services.AddHttpForwarderWithServiceDiscovery();
       
    }

    public static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
    {
    }
}

