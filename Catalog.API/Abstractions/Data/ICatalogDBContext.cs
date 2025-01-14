namespace Catalog.API.Abstractions.Data
{
    public interface ICatalogDBContext
    {
        DbSet<Tenant> Tenant { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
