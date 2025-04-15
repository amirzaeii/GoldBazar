using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.IdentityModel.JsonWebTokens;
using MudBlazor.Services;
using WebComponent.Services;
using WebComponent.Extensions;

namespace WebApp.Extensions;
public static class Extensions
{
    public static void AddWebApplicationServices(this IHostApplicationBuilder builder)
    {
     
      builder.AddAuthenticationServices();
      
      builder.Services.AddMudServices();
      builder.Services.AddBlazoredLocalStorage();
      
      builder.Services.AddScoped<LocalStorageService>();

        builder.AddRabbitMqEventBus("EventBus")
               .AddEventBusSubscriptions();

        builder.Services.AddHttpForwarderWithServiceDiscovery();

        // Application services
        builder.Services.AddScoped<BasketState>();
        //builder.Services.AddScoped<LogOutService>();
        builder.Services.AddSingleton<BasketService>();
        builder.Services.AddSingleton<OrderStatusNotificationService>();
        builder.Services.AddSingleton<IProductImageUrlProvider, ProductImageUrlProvider>();
        //builder.AddAIServices();

        // HTTP and GRPC client registrations
       builder.Services.AddGrpcClient<Basket.Api.Grpc.Basket.BasketClient>(o => o.Address = new("http://basket-api"));
        // .AddAuthToken();
        builder.Services.AddHttpClient<CatalogService>(o => o.BaseAddress = new("http://catalog-api"))
            .AddApiVersion(1.0);
        //.AddAuthToken();
        builder.Services.AddHttpClient<IShopService ,ShopService>(o => o.BaseAddress = new("http://catalog-api"))
            .AddApiVersion(1.0);

         builder.Services.AddHttpClient<OrderingService>(o => o.BaseAddress = new("http://ordering-api"))
             .AddApiVersion(1.0);
        //     .AddAuthToken();

        builder.Services.AddHttpClient<ICatalogService, CatalogService>(o => o.BaseAddress = new("http://catalog-api"))
    .AddApiVersion(1.0);

    }

    public static void AddAuthenticationServices(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        var identityUrl = configuration.GetRequiredValue("IdentityUrl");
        var callBackUrl = configuration.GetRequiredValue("CallBackUrl");
        var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

        // Add Authentication services
        services.AddAuthorization();
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie(options => options.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
        .AddOpenIdConnect(options =>
        {
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.Authority = identityUrl;
            options.SignedOutRedirectUri = callBackUrl;
            options.ClientId = "webapp";
            options.ClientSecret = "secret";
            options.ResponseType = "code";
            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.RequireHttpsMetadata = false;
            options.Scope.Add("openid");
            options.Scope.Add("profile");
            options.Scope.Add("orders");
            options.Scope.Add("basket");
        });

        // Blazor auth services
        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        services.AddCascadingAuthenticationState();
    }

    // private static void AddAIServices(this IHostApplicationBuilder builder)
    // {
    //     string? ollamaEndpoint = builder.Configuration["AI:Ollama:Endpoint"];
    //     if (!string.IsNullOrWhiteSpace(ollamaEndpoint))
    //     {
    //         builder.Services.AddChatClient(new OllamaChatClient(ollamaEndpoint, builder.Configuration["AI:Ollama:ChatModel"] ?? "llama3.1"))
    //             .UseFunctionInvocation()
    //             .UseOpenTelemetry(configure: t => t.EnableSensitiveData = true)
    //             .UseLogging()
    //             .Build();
    //     }
    //     else
    //     {
    //         var chatModel = builder.Configuration.GetSection("AI").Get<AIOptions>()?.OpenAI?.ChatModel;
    //         if (!string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("openai")) && !string.IsNullOrWhiteSpace(chatModel))
    //         {
    //             builder.AddOpenAIClientFromConfiguration("openai");
    //             builder.Services.AddChatClient(sp => sp.GetRequiredService<OpenAIClient>().AsChatClient(chatModel ?? "gpt-4o-mini"))
    //                 .UseFunctionInvocation()
    //                 .UseOpenTelemetry(configure: t => t.EnableSensitiveData = true)
    //                 .UseLogging()
    //                 .Build();
    //         }
    //     }
    // }

    
}