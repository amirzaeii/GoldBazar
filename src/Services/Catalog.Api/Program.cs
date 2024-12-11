using AutoMapper;
using Catalog.Api.Profiles;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);
//builder.AddServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();

var withApiVersioning = builder.Services.AddApiVersioning();

builder.AddDefaultOpenApi(withApiVersioning);

// AutoMapper Configuration: Added this section
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

//app.MapDefaultEndpoints();

app.NewVersionedApi("Catalog")
   .MapCatalogApiV1();

//app.UseDefaultOpenApi();
app.Run();