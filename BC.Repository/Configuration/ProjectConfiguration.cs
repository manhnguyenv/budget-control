using BC.Repository.Domain;
using System.Data.Entity.ModelConfiguration;

namespace BC.Repository.Configuration
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>, IConfiguration
    {
        public ProjectConfiguration()
        {
            ToTable("Project");
            HasKey(k => k.Id);
            Property(p => p.Id).HasColumnName("IdProject");
            Property(p => p.Description).IsRequired().HasMaxLength(80);
            Property(p => p.IdDepartment).IsRequired();

            HasRequired(f => f.Department);
        }
    }
}
