using BC.Repository.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BC.Repository.Configuration
{
    public class BudgetConfiguration : EntityTypeConfiguration<Budget>, IConfiguration
    {
        public BudgetConfiguration()
        {
            ToTable("Budget");
            HasKey(k => k.IdDepartment);
            HasKey(k => k.Year);
            Property(p => p.IdDepartment);
            Property(p => p.Year);
            Property(p => p.Value).IsRequired().HasPrecision(15,2);
            HasRequired(f => f.Department);
        }
    }
}
