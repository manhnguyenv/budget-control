using BC.Contracts.Repository;
using BC.Contracts.Services;
using BC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace BC.Service
{
    public class SupplierService : ISupplierService
    {
        private IUnitOfWork context;

        public SupplierService(IUnitOfWork _context)
        {
            context = _context;
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            if(QueryCacheManager.Cache["SUPPLIERS"] == null) QueryCacheManager.Cache["SUPPLIERS"] = context.SupplierRepository.GetAll();
            return (IEnumerable<Supplier>) QueryCacheManager.Cache["SUPPLIERS"];
        }
    }
}
