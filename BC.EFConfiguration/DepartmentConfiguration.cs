using BC.Domain;
using BC.Contracts.Configuration;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC.EFConfiguration
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>, IConfiguration
    {
        public DepartmentConfiguration()
        {
            ToTable("Department");
            HasKey(k => k.Id);
            Property(p => p.Id).HasColumnName("IdDepartment").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Description).HasMaxLength(80).IsRequired();
            Property(p => p.IdDepartmentParent).IsOptional();
            HasOptional(f => f.DepartmentParent).WithMany(f => f.Departments).HasForeignKey(f => f.IdDepartmentParent);
        }
    }
}
