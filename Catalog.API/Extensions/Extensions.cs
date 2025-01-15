
using System.Text.Json;
using Npgsql.Replication;

namespace Catalog.API.Extensions
{
    public static class Extensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
                                                        
            builder.AddNpgsqlDbContext<CatalogContext>(connectionName: "catalogdb", configureDbContextOptions: dbContextOptionsBuilder => 
            {
                
                dbContextOptionsBuilder.UseSeeding((context, _) =>
                {
                   
                    var licenseJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "licenses.json");
                    var licenseData = File.ReadAllText(licenseJsonPath);
                    var licenses = JsonSerializer.Deserialize<List<License>>(licenseData);
                    var tenantJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "tenants.json");
                    var tenantData = File.ReadAllText(tenantJsonPath);
                    var tenants = JsonSerializer.Deserialize<List<Tenant>>(tenantData);
                   
                    if (licenses != null)
                    {
                       
                        context.Set<License>().AddRange(licenses);
                        context.SaveChanges();
                    }
                    if (tenants != null)
                    {
                        context.Set<Tenant>().AddRange(tenants);
                        context.SaveChanges();
                    }


                });
            });
            
        
        }
        
    }
}