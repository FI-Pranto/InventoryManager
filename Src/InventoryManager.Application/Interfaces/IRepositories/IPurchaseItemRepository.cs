using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Application.Interfaces.IRepositories
{
    public interface IPurchaseItemRepository : IRepository<PurchaseItem>
    {
        void Update(PurchaseItem item);

    }
}
