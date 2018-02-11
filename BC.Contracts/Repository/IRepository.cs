using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BC.Contracts.Repository
{
    public interface IRepository<T> where T : class
    {
        void Save(T model);
        void Update(T model);
        void Delete(T model);
        IQueryable<T> GetAll();
        T GetById(object id);
        IQueryable<T> Where(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] navigationProperties);
    }
}
