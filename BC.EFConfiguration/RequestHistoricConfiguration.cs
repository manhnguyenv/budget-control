using BC.Domain;
using BC.Contracts.Configuration;
using System.Data.Entity.ModelConfiguration;

namespace BC.EFConfiguration
{
    public class RequestHistoricConfiguration : EntityTypeConfiguration<RequestHistoric>, IConfiguration
    {
        public RequestHistoricConfiguration()
        {
            ToTable("RequestHistoric");
            HasKey(k => new { k.IdRequest, k.RequestDate });
            Property(p => p.IdRequest);
            Property(p => p.RequestDate);
            Property(p => p.RequestStatus).IsRequired();
            HasRequired(f => f.Request).WithMany(f => f.RequestsHistoric).HasForeignKey(f => f.IdRequest);
        }
    }
}
