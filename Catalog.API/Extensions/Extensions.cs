
using Npgsql.Replication;

namespace Catalog.API.Extensions
{
    public static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
           
            builder.AddNpgsqlDbContext<CatalogContext>(connectionName: "catalogdb");
        
        }
        
    }
}