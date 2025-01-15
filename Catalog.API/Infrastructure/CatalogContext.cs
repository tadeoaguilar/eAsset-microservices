
using System.Data.Common;
using System.Reflection;


namespace Catalog.API.Infrastructure
{
    public class CatalogContext : DbContext//, ICatalogDBContext

    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options) { }

        public DbSet<Tenant> Tenant => Set<Tenant>();
        public DbSet<Company> Company => Set<Company>();
        public DbSet<Organization> Organization => Set<Organization>();
        public DbSet<Location> Location => Set<Location>();
        public DbSet<Asset> Asset => Set<Asset>();
        public DbSet<License> License => Set<License>();
        public DbSet<Manufacturer> Manufacturer => Set<Manufacturer>();




        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.HasDefaultSchema("catalog");
            

            base.OnModelCreating(builder);

        }

        //TODO- Include Database Extension to create Database at startup
    }
}
