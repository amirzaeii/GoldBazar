
var builder = WebApplication.CreateSlimBuilder(args);

builder.AddBasicServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();

var withApiVersioning = builder.Services.AddApiVersioning();

builder.AddDefaultOpenApi(withApiVersioning);

var app = builder.Build();

app.MapDefaultEndpoints();

var orders = app.NewVersionedApi("Orders");

orders.MapOrdersApiV1();
      //.RequireAuthorization();

app.UseDefaultOpenApi();

app.Run();


