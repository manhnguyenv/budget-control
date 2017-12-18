using BC.Repository.Domain;

namespace BC.Repository.Context
{
    public class RequestRepository : BaseContext<Request>, IUnitOfWork<Request>
    {
    }
}
