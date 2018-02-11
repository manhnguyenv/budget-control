using BC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Contracts.Services
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAllSuppliers();
    }
}
