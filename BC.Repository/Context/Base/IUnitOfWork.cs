using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BC.Repository.Context
{
    public interface IUnitOfWork<T> where T : class
    {
        int Save(T model);
        int Update(T model);
        void Delete(T model);
        IEnumerable<T> GetAll();
        T GetById(object id);
        IEnumerable<T> Where(Expression<Func<T, bool>> expression, params string[] navigationProperties);
    }
}
