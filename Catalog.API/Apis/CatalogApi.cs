namespace Catalog.API.Apis
{
    public static class CatalogApi
    {
        public static IEndpointRouteBuilder MapCatalogApi(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/catalog/assets", async (CatalogContext db) =>
                await db.Asset.ToListAsync());
            return app;
        }
    
    
    }
}