using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;

namespace InventoryManager.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(ISupplierRepository supplierRepository,IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }
        public void AddSupplier(Supplier supplier)
        {
            _supplierRepository.Add(supplier);
            _unitOfWork.Commit();
        }

        public IEnumerable<Supplier> GetAllSuppliers(string? searchTerm,string? includeProp = null, int page = 1,int pageSize=1, bool pagination = false, bool descending = false)
        {
            if(searchTerm == null)
            {
                return _supplierRepository.GetAll(null,includeProp: includeProp,page,pageSize:pageSize,pagination: pagination, u=>u.Name,descending);
            }

            searchTerm = searchTerm.ToLower();

            return _supplierRepository.GetAll(u=>u.Name.ToLower().Contains(searchTerm),includeProp: includeProp,page,pageSize:pageSize, pagination: pagination, u => u.Name, descending);
        }

       public int TotalPages(string? searchTerm,int pageSize)
        {
            int totalItems = _supplierRepository.Count(searchTerm);
            return (int)Math.Ceiling(totalItems / (double)pageSize);
        }
        public (int startPage, int endPage) GetStartAndEnd(string? searchTerm,int page=1,int pageSize=1)
        {
            int totalPages = TotalPages(searchTerm,pageSize: pageSize);


            int startPage = Math.Max(1, page - 1);
            int endPage = Math.Min(totalPages, startPage + 2);

            if (endPage - startPage < 2)
            {
                startPage = Math.Max(1, endPage - 2);
            }
            return (startPage, endPage);
        }

        public Supplier? GetSupplierById(int? id, string? includeProp = null)
        {
            return _supplierRepository.Get(u => u.Id == id,includeProp);
        }

        public void RemoveSupplier(Supplier supplier)
        {
            _supplierRepository.Remove(supplier);
            _unitOfWork.Commit();
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _supplierRepository.Update(supplier);
            _unitOfWork.Commit();
        }
    }
}
