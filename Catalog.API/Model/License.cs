namespace Catalog.API.Model
{
    public class License : Entity<Guid>
    {
        public string LicenseCD { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public bool Active { get; set; } = default!;
        public ICollection<Tenant> Tenants { get; } = new List<Tenant>();
    }
}
