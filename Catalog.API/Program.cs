using Catalog.API.Apis;
using Catalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.AddServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "CatalogAPI";
    config.Title = "CatalogAPI v1";
    config.Version = "v1";
});
var withApiVersioning = builder.Services.AddApiVersioning();


var app = builder.Build();
    app.MapDefaultEndpoints();
    app.MapCatalogApi();
    
if (app.Environment.IsDevelopment())
{

    app.MapOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "CatalogAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.UseHttpsRedirection();





app.Run();

