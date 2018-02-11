using BC.Repository.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BC.Repository.Configuration
{
    public class SupplierConfiguration : EntityTypeConfiguration<Supplier>, IConfiguration
    {
        public SupplierConfiguration()
        {
            ToTable("Supplier");
            HasKey(k => k.Id);
            Property(p => p.Id).HasColumnName("IdSupplier");
            Property(p => p.Name).IsRequired().HasMaxLength(80);
        }
    }
}
