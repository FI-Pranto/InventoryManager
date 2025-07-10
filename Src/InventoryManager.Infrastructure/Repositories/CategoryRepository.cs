using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Domain.Entities;
using InventoryManager.Infrastructure.Data;
using InventoryManager.Infrastructure.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
        public int Count(string? searchTerm=null)
        {
            IQueryable<Category> query = _db.Categories;
            if(searchTerm != null)
            {
                query=query.Where(u=>u.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            return query.Count();
            

        }
    }
}
