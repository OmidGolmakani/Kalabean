using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Kalabean.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        T Delete(T entity);
        T Update(T entity);
        void UpdateBatch(IEnumerable<T> entity);
        IQueryable<T> List(Expression<Func<T, bool>> expression);
    }
}
