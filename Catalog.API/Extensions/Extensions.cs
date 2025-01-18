
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

                    var countryJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "countries.json");
                    var countryData = File.ReadAllText(countryJsonPath);
                    var countries = JsonSerializer.Deserialize<List<Country>>(countryData);

                    var companyJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "companies.json");
                    var companyData = File.ReadAllText (companyJsonPath);
                    var companies = JsonSerializer.Deserialize<List<Company>> (companyData);

                    var organizationJsonPath = Path.Combine(AppContext.BaseDirectory, "Setup", "organizations.json");
                    var organizationData = File.ReadAllText (organizationJsonPath);
                    var organizations = JsonSerializer.Deserialize<List<Organization>> (organizationData);


                   
                    if (licenses != null)
                    {
                        context.Set<License>().RemoveRange(context.Set<License>());
                        context.Set<License>().AddRange(licenses);
                        context.SaveChanges();
                    }
                    if (tenants != null)
                    {
                        context.Set<Tenant>().RemoveRange(context.Set<Tenant>());
                        context.Set<Tenant>().AddRange(tenants);
                        context.SaveChanges();
                    }
                    if (countries != null)
                    {
                        context.Set<Country>().RemoveRange(context.Set<Country>());
                        context.Set<Country>().AddRange(countries);
                        context.SaveChanges();
                    }
                    if (companies != null)
                    {
                        context.Set<Company>().RemoveRange(context.Set<Company>());
                        context.Set<Company>().AddRange(companies);
                        context.SaveChanges();
                    }
                    if (organizations != null)
                    {
                        context.Set<Organization>().RemoveRange(context.Set<Organization>());
                        context.Set<Organization>().AddRange(organizations);
                        context.SaveChanges();
                    }



                });
            });
            
        
        }
        
    }
}