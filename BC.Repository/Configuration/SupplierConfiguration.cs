using BC.Repository.Domain;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC.Repository.Configuration
{
    public class SupplierConfiguration : EntityTypeConfiguration<Supplier>, IConfiguration
    {
        public SupplierConfiguration()
        {
            ToTable("Supplier");
            HasKey(k => k.Id);
            Property(p => p.Id).HasColumnName("IdSupplier").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).IsRequired().HasMaxLength(80);
        }
    }
}
