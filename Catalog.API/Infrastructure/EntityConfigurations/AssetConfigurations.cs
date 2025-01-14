


namespace Catalog.API.Infrastructure.EntityConfigurations
{
    public class AssetConfigurations : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(p => new { p.AssetCD, p.SubNumber });
        }
    }
}
