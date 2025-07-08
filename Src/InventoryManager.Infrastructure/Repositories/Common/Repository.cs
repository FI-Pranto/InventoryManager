using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Infrastructure.Repositories.Common
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _dbSet = db.Set<T>();

        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T? Get(Expression<Func<T, bool>> filter, string? includeProp = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter!=null)
            {
                query = query.Where(filter);
            }

            if (includeProp != null){

                foreach(var prop in includeProp.Split(','))
                {
                    query = query.Include(prop);
                }

            }
            return query.FirstOrDefault();


        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProp = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProp != null)
            {

                foreach (var prop in includeProp.Split(','))
                {
                    query = query.Include(prop);
                }

            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
