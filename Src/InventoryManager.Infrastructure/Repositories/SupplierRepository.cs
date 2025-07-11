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
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        private readonly ApplicationDbContext _db;

        public SupplierRepository(ApplicationDbContext db) : base(db)
        {
             _db = db;
        }
        public int Count(string? searchTerm = null)
        {
            IQueryable<Supplier> query = _db.Suppliers;

            if (searchTerm != null)
            {
                query = query.Where(u => u.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            return query.Count();
        }

        public void Update(Supplier supplier)
        {
            _db.Suppliers.Update(supplier);
        }
    }
}
