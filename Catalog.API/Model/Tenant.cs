
namespace Catalog.API.Model
{
    public class Tenant : Entity<Guid>
    {
        public string TenantName { get; set; } = default!;
        public string TenantCD { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public DateTime StartDate { get; set; } = default!;
        public DateTime EndDate { get; set; } = default!;
        public License License { get; set; } = default!;
        public Guid LicenseID { get; set; } = default!;
        public bool Active { get; set; } = default!;

        public ICollection<Company> Companies { get; } = new List<Company>();

        public static Tenant Create(Guid id, string tenantName, string tenantCD, string description, DateTime startDate, DateTime endDate,Guid licenseId, bool active)
        {



            var tenant = new Tenant
            {
                Id = id,
                TenantName = tenantName,
                TenantCD = tenantCD,
                Description = description,                 
                StartDate = startDate,
                EndDate = endDate,
                Active = active,
                LicenseID = licenseId
            };



            return tenant;
        }
    }
}
