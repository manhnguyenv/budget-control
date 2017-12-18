using BC.Repository.Domain;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC.Repository.Configuration
{
    public class RequestConfiguration : EntityTypeConfiguration<Request>, IConfiguration
    {
        public RequestConfiguration()
        {
            ToTable("Request");
            HasKey(k => k.Id);
            Property(p => p.Id).HasColumnName("IdRequest").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.IdProject);
            Property(p => p.IdSupplier).IsRequired();
            Property(p => p.RequestDate).IsRequired();
            Property(p => p.Value).HasPrecision(15,2).IsRequired();
            Property(p => p.RequestStatus).IsRequired();
            HasRequired(f => f.Project).WithMany(f => f.Requests).HasForeignKey(f => f.IdProject);
            HasRequired(f => f.Supplier).WithMany(f => f.Requests).HasForeignKey(f => f.IdSupplier);
        }
    }
}
