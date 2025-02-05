using Catalog.API.Apis;
using Catalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "CatalogAPI";
    config.Title = "CatalogAPI v1";
    config.Version = "v1";
});*/

//builder.Services.AddOpenApi();
//var withApiVersioning = builder.Services.AddApiVersioning();


var app = builder.Build();
    app.MapDefaultEndpoints();
    app.MapCatalogApi();
    
if (app.Environment.IsDevelopment())
{

    //app.MapOpenApi();
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
}

app.UseHttpsRedirection();





app.Run();

