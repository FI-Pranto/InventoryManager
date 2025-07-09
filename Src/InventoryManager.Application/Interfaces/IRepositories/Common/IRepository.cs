using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Application.Interfaces.IRepositories.Common
{
    public interface IRepository<T> where T : class
    {
        T ? Get(Expression<Func<T, bool>> filter,string? includeProp=null);

        IEnumerable<T> GetAll<TKey>(Expression<Func<T, bool>>? filter = null, string? includeProp = null, int page = 1,
            int pageSize = 1, Expression<Func<T, TKey>>? orderBy = null, bool descending = false);

        void Add(T entity);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
