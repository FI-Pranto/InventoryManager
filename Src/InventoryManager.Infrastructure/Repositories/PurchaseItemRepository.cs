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
    public class PurchaseItemRepository : Repository<PurchaseItem>, IPurchaseItemRepository
    {
        private readonly ApplicationDbContext _db;

        public PurchaseItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(PurchaseItem item)
        {
            _db.PurchaseItems.Update(item);
        }
    }
}
