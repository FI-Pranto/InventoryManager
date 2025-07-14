using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Domain.Entities;
using InventoryManager.Infrastructure.Data;
using InventoryManager.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Infrastructure.Repositories
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        private readonly ApplicationDbContext _db;

        public PurchaseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int Count(string? searchTerm = null)
        {
            IQueryable<Purchase> query = _db.Purchases.Include(u=>u.Supplier);

            if (searchTerm != null)
            {
                query = query.Where(u => u.Supplier.Name.ToLower().Contains(searchTerm) || u.PurchaseDate.ToString("g").ToLower().Contains(searchTerm));
            }
            return query.Count();
            
        }

        public void Update(Purchase item)
        {
            _db.Purchases.Update(item);
        }
    }
}
