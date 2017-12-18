using BC.Repository.Domain;

namespace BC.Repository.Context
{
    public class BudgetRepository : BaseContext<Budget>, IUnitOfWork<Budget>
    {
    }
}
