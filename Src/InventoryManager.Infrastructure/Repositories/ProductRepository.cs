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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
        }
        public int Count(string? searchTerm=null)
        {
            IQueryable<Product> query = _db.Products;
            if(searchTerm != null)
            {
                query=query.Where(u=>u.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            return query.Count();
            

        }
 
    }
}
