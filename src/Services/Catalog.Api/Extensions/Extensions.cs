
namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<CatalogContext>("catalogdb", configureDbContextOptions: dbContextOptionsBuilder =>
        {
            dbContextOptionsBuilder.UseNpgsql(builder =>
            {
                //builder.UseVector();
            });
        });

        //REVIEW: This is done for development ease but shouldn't be here in production
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddMigration<CatalogContext, CatalogDevelopmentContextSeed>();
        }
        else
        {
            builder.Services.AddMigration<CatalogContext, CatalogContextSeed>();
        }


        // Add the integration services that consume the DbContext
        builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<CatalogContext>>();

        builder.Services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

        builder.AddRabbitMqEventBus("gb-eventbus")
               .AddSubscription<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>()
               .AddSubscription<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();

        //builder.Services.AddScoped<IFileService, LocalFileService>();
        // builder.Services.AddOptions<CatalogOptions>()
        //     .BindConfiguration(nameof(CatalogOptions));

        // if (builder.Configuration["AI:Ollama:Endpoint"] is string ollamaEndpoint && !string.IsNullOrWhiteSpace(ollamaEndpoint))
        // {
        //     builder.Services.AddEmbeddingGenerator(new OllamaEmbeddingGenerator(ollamaEndpoint, builder.Configuration["AI:Ollama:EmbeddingModel"]))
        //         .UseOpenTelemetry()
        //         .UseLogging()
        //         .Build();
        // }
        // else if (!string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("openai")))
        // {
        //     builder.AddOpenAIClientFromConfiguration("openai");
        //     builder.Services.AddEmbeddingGenerator(sp => sp.GetRequiredService<OpenAIClient>().AsEmbeddingGenerator(builder.Configuration["AI:OpenAI:EmbeddingModel"]!))
        //         .UseOpenTelemetry()
        //         .UseLogging()
        //         .Build();
        // }

        // builder.Services.AddScoped<ICatalogAI, CatalogAI>();
    }
}
