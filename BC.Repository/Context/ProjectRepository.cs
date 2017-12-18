using BC.Repository.Domain;

namespace BC.Repository.Context
{
    public class ProjectRepository : BaseContext<Project>, IUnitOfWork<Project>
    {
    }
}
