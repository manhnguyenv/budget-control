using BC.Repository.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BC.Repository.Configuration
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>, IConfiguration
    {
        public DepartmentConfiguration()
        {
            ToTable("Department");
            HasKey(k => k.Id);
            Property(p => p.Id).HasColumnName("IdDepartment");
            Property(p => p.Description).HasMaxLength(80).IsRequired();
            Property(p => p.IdDepartmentParent).IsOptional();
            HasOptional(f => f.DepartmentParent).WithMany().HasForeignKey(f => f.IdDepartmentParent);
        }
    }
}
