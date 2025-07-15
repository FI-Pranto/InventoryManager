using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Application.Interfaces.IServices
{
    public interface IPurchaseService
    {
        void AddPurchase(Purchase purchase);
        void RemovePurchase(Purchase purchase);
        void UpdatePurchase(Purchase purchase);

        Purchase? GetPurchaseById(int? id, string? includeProp = null);

        IEnumerable<Purchase> GetAllPurchases(string? searchTerm, string? includeProp = null, int page = 1, int pageSize = 1, bool pagination = false, bool descending = false);

        int TotalPages(string? searchTerm, int pageSize);

        (int startPage, int endPage) GetStartAndEnd(string? searchTerm, int page = 1, int pageSize = 1);

        (IEnumerable<SelectListItem> supplierList, IEnumerable<SelectListItem> productList) GetSupplierAndProduct();


    }
}
