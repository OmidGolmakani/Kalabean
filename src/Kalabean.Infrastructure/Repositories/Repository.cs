using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Kalabean.Infrastructure.Repositories
{
   public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbFactory _dbFactory;
        private DbSet<T> _dbSet;
        protected DbSet<T> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set<T>());
        }
        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public T Add(T entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public T Delete(T entity)
        {
            return DbSet.Remove(entity).Entity;
        }

        public IQueryable<T> List(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression).AsNoTracking();
        }

        public T Update(T entity)
        {
            return DbSet.Update(entity).Entity;
        }

        public void UpdateBatch(System.Collections.Generic.IEnumerable<T> entities)
        {
            DbSet.UpdateRange(entities);
        }
    }
}
