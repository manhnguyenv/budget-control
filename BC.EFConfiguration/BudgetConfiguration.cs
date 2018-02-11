using BC.Domain;
using BC.Contracts.Configuration;
using System.Data.Entity.ModelConfiguration;

namespace BC.EFConfiguration
{
    public class BudgetConfiguration : EntityTypeConfiguration<Budget>, IConfiguration
    {
        public BudgetConfiguration()
        {
            ToTable("Budget");
            HasKey(k => new { k.IdDepartment, k.Year });
            Property(p => p.IdDepartment);
            Property(p => p.Year);
            Property(p => p.Value).IsRequired().HasPrecision(15,2);
            HasRequired(f => f.Department).WithMany().HasForeignKey(f => f.IdDepartment);
        }
    }
}
