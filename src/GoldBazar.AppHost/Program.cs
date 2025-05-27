using GoldBazar.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();

var redis = builder.AddRedis("gb-redis");
var rabbitMq = builder.AddRabbitMQ("gb-eventbus")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithManagementPlugin();

var postgres = builder.AddPostgres("gb-database")
    //.WithImage("ankane/pgvector")
    //.WithPgAdmin()
    .WithImageTag("latest")
    .WithLifetime(ContainerLifetime.Persistent);

var catalogDb = postgres.AddDatabase("catalogdb");
var identityDb = postgres.AddDatabase("identitydb");
var orderDb = postgres.AddDatabase("orderingdb");
// var webhooksDb = postgres.AddDatabase("webhooksdb");

var launchProfileName = ShouldUseHttpForEndpoints() ? "http" : "https";

// Services
var identityApi = builder.AddProject<Projects.Identity_Api>("identity-api", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(identityDb)
    .WaitFor(identityDb);

var identityEndpoint = identityApi.GetEndpoint(launchProfileName);

var basketApi = builder.AddProject<Projects.Basket_Api>("basket-api")
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithEnvironment("Identity__Url", identityEndpoint);

var catalogApi = builder.AddProject<Projects.Catalog_Api>("catalog-api")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithReference(catalogDb)
    .WaitFor(catalogDb)
    .WithHttpHealthCheck("/health")
    .WithEnvironment("Identity__Url", identityEndpoint); ;

var orderingApi = builder.AddProject<Projects.Ordering_Api>("ordering-api")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithReference(orderDb)
    .WaitFor(orderDb)
    .WithHttpHealthCheck("/health")
    .WithEnvironment("Identity__Url", identityEndpoint);

builder.AddProject<Projects.Order_Processor>("order-processor")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithReference(orderDb)
    .WaitFor(orderingApi); // wait for the orderingApi to be ready because that contains the EF migrations


// var webHooksApi = builder.AddProject<Projects.Webhooks_API>("webhooks-api")
//     .WithReference(rabbitMq).WaitFor(rabbitMq)
//     .WithReference(webhooksDb)
//     .WithEnvironment("Identity__Url", identityEndpoint);

// Reverse proxies
builder.AddProject<Projects.Mobile_Bff>("mobile-bff")
    .WithReference(catalogApi)
    .WaitFor(catalogApi)
    .WithReference(orderingApi)
    .WaitFor(orderingApi)
    .WithReference(basketApi)
    .WaitFor(basketApi)
    .WithReference(identityApi)
    .WaitFor(identityApi);

// Apps
// var webhooksClient = builder.AddProject<Projects.WebhookClient>("webhooksclient", launchProfileName)
//     .WithReference(webHooksApi)
//     .WithEnvironment("IdentityUrl", identityEndpoint);

var clientWebApp = builder.AddProject<Projects.GoldBazar_Client_Web>("gb-client-web", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(basketApi)
    .WaitFor(basketApi)
    .WithReference(catalogApi)
    .WaitFor(catalogApi)
    .WithReference(orderingApi)
    .WaitFor(orderingApi)
    .WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithEnvironment("IdentityUrl", identityEndpoint);

var vendorWebApp = builder.AddProject<Projects.GoldBazar_Vendor_Web>("gb-vendor-web", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(catalogApi)
    .WaitFor(catalogApi)
    .WithReference(orderingApi)
    .WaitFor(orderingApi)
    .WithReference(rabbitMq).WaitFor(rabbitMq)
    .WithEnvironment("IdentityUrl", identityEndpoint);

var adminwebApp = builder.AddProject<Projects.GoldBazar_Admin_Web>("gb-admin-web", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(catalogApi)
    .WithReference(orderingApi)
    .WithReference(rabbitMq).WaitFor(rabbitMq);

// set to true if you want to use OpenAI
// bool useOpenAI = false;
// if (useOpenAI)
// {
//     builder.AddOpenAI(catalogApi, webApp);
// }

// Wire up the callback urls (self referencing)
clientWebApp.WithEnvironment("CallBackUrl", clientWebApp.GetEndpoint(launchProfileName));
//webhooksClient.WithEnvironment("CallBackUrl", webhooksClient.GetEndpoint(launchProfileName));

// Identity has a reference to all of the apps for callback urls, this is a cyclic reference
identityApi.WithEnvironment("BasketApiClient", basketApi.GetEndpoint("http"))
           .WithEnvironment("OrderingApiClient", orderingApi.GetEndpoint("http"))
           //    .WithEnvironment("WebhooksApiClient", webHooksApi.GetEndpoint("http"))
           //    .WithEnvironment("WebhooksWebClient", webhooksClient.GetEndpoint(launchProfileName))
           .WithEnvironment("WebAppClient", clientWebApp.GetEndpoint(launchProfileName));

builder.Build().Run();

// For test use only.
// Looks for an environment variable that forces the use of HTTP for all the endpoints. We
// are doing this for ease of running the Playwright tests in CI.
static bool ShouldUseHttpForEndpoints()
{
    const string EnvVarName = "GOLDBAZAR_USE_HTTP_ENDPOINTS";
    var envValue = Environment.GetEnvironmentVariable(EnvVarName);

    // Attempt to parse the environment variable value; return true if it's exactly "1".
    return int.TryParse(envValue, out int result) && result == 1;
}
