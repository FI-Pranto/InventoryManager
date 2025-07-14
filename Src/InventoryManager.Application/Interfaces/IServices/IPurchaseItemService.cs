using InventoryManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Application.Interfaces.IServices
{
    public interface IPurchaseItemService
    {
        void AddPurchaseItem(PurchaseItem purchaseItem);
        void RemovePurchaseItem(PurchaseItem purchaseItem);
        void UpdatePurchaseItem(PurchaseItem purchaseItem);

        PurchaseItem? GetPurchaseItemById(int? id, string? includeProp = null);

        IEnumerable<PurchaseItem> GetAllPurchaseItems(string? searchTerm, string? includeProp = null, int page = 1, int pageSize = 1, bool pagination = false, bool descending = false);

    }
}
