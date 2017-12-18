using BC.Repository.Domain;

namespace BC.Repository.Context
{
    public class SupplierRepository : BaseContext<Supplier>, IUnitOfWork<Supplier>
    {
    }
}
