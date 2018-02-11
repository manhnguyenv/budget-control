using BC.Domain;
using BC.Contracts.Configuration;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC.EFConfiguration
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
