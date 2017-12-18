using BC.Repository.Domain;

namespace BC.Repository.Context
{
    public class DepartmentRepository : BaseContext<Department>, IUnitOfWork<Department>
    {
    }
}
