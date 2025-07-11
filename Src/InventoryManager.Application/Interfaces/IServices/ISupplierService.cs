using InventoryManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Application.Interfaces.IServices
{
    public interface ISupplierService
    {
        void AddSupplier(Supplier supplier);
        void UpdateSupplier(Supplier supplier);

        void RemoveSupplier(Supplier supplier);

        Supplier? GetSupplierById(int? id,string? includeProp=null);

        IEnumerable<Supplier> GetAllSuppliers(string? searchTerm,string? includeProp=null,int page=1, int pageSize = 1, bool pagination = false, bool descending = false);

        int TotalPages(string? searchTerm, int pageSize);

        (int startPage, int endPage) GetStartAndEnd(string? searchTerm, int page = 1,int pageSize=1);


    }
}
