namespace Microsoft.Extensions.DependencyInjection;
public static class Extensions
{
    public static void AddAdminAppApplicationServices(this IHostApplicationBuilder builder)
    {
        //  builder.AddAuthenticationServices();

        builder.AddRabbitMqEventBus("gb-eventbus")
               .AddEventBusSubscriptions();
       
    }

    public static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
    {
    }
}

