using Ordering.Api.Application.DomainEventHandlers;
using Ordering.Api.Infrastructure;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        
        // Add the authentication services to DI
        builder.AddDefaultAuthentication();

        // Pooling is disabled because of the following error:
        // Unhandled exception. System.InvalidOperationException:
        // The DbContext of type 'OrderingContext' cannot be pooled because it does not have a public constructor accepting a single parameter of type DbContextOptions or has more than one constructor.
        services.AddDbContext<OrderingContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("orderingdb"));
        });
        builder.EnrichNpgsqlDbContext<OrderingContext>();

        // Add the integration services that consume the DbContext
        services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<OrderingContext>>();

        services.AddTransient<IOrderingIntegrationEventService, OrderingIntegrationEventService>();

        builder.AddRabbitMqEventBus("gb-eventbus")
               .AddEventBusSubscriptions();

        services.AddHttpContextAccessor();
        services.AddTransient<IIdentityService, IdentityService>();

        // Configure mediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        // Register the command validators for the validator behavior (validators based on FluentValidation library)
        services.AddSingleton<IValidator<CancelOrderByBuyerCommand>, CancelOrderByBuyerCommandValidator>();
        services.AddSingleton<IValidator<CancelOrderByVendorCommand>, CancelOrderByVendorCommandValidator>();
        services.AddSingleton<IValidator<CancelOrderByGBCommand>, CancelOrderByGBCommandValidator>();
        services.AddSingleton<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
        services.AddSingleton<IValidator<IdentifiedCommand<CreateOrderCommand, bool>>, IdentifiedCommandValidator>();
        services.AddSingleton<IValidator<ShippedOrderCommand>, ShippedOrderCommandValidator>();
        services.AddSingleton<IValidator<SetShippingOrderStatusCommand>, ShippingOrderCommandValidator>();

        services.AddScoped<IOrderQueries, OrderQueries>();
        services.AddScoped<IBuyerRepository, BuyerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IRequestManager, RequestManager>();
    }

    private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
    {
        eventBus.AddSubscription<OrderStatusChangedToAwaitingValidationFromVendorIntegrationEvent, OrderStatusChangedToAwaitingValidationFromVendorIntegrationEventHandler>();
        eventBus.AddSubscription<OrderStatusChangedToAwaitingValidationFromGBIntegrationEvent, OrderStatusChangedToAwaitingValidationFromGBIntegrationEventHandler>();
        eventBus.AddSubscription<OrderStatusChangedToCancelledByBuyerIntegrationEvent, OrderStatusChangedToCancelledByBuyerIntegrationEventHandler>();
        eventBus.AddSubscription<OrderStatusChangedToCancelledByVendorIntegrationEvent, OrderStatusChangedToCancelledByVendorIntegrationEventHandler>();
        eventBus.AddSubscription<OrderStatusChangedToCancelledByGBIntegrationEvent, OrderStatusChangedToCancelledByGBIntegrationEventHandler>();
        eventBus.AddSubscription<OrderStatusChangedToShippedIntegrationEvent, OrderStatusChangedToShippedIntegrationEventHandler>();
        eventBus.AddSubscription<OrderStatusChangedToShippingIntegrationEvent, OrderStatusChangedToShippingIntegrationEventHandler>();
    }
}
