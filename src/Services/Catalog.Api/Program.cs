using Catalog.Api.Apis;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();

var withApiVersioning = builder.Services.AddApiVersioning();

builder.AddDefaultOpenApi(withApiVersioning);

var app = builder.Build();

app.MapDefaultEndpoints();

app.NewVersionedApi("Catalog")
   .MapCatalogApiV1();

app.NewVersionedApi("CatalogInfo")
   .MapCatalogInfoApiV1();

app.UseDefaultOpenApi();
app.Run();