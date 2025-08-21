namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{

    public static void AddClientWebAppApplicationServices(this IHostApplicationBuilder builder)
    {
        //  builder.AddAuthenticationServices();
        builder.AddShareApplicationServices();
        //builder.AddRabbitMqEventBus("gb-eventbus")
              // .AddEventBusSubscriptions();

    }

    public static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
    {
    }
}
