using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var withApiVersioning = builder.Services.AddApiVersioning();

//builder.AddDefaultOpenApi(withApiVersioning);

var app = builder.Build();

app.MapDefaultEndpoints();

if(app.Environment.IsDevelopment()){
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.NewVersionedApi("Catalog").MapCatalogApiV1();

//app.UseDefaultOpenApi();
app.Run();