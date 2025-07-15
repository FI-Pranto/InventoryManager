using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManager.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IPurchaseRepository purchaseRepository,ISupplierRepository supplierRepository,IProductRepository productRepository,IUnitOfWork unitOfWork)
        {
            _purchaseRepository = purchaseRepository;
            _supplierRepository = supplierRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public void AddPurchase(Purchase purchase)
        {
            _purchaseRepository.Add(purchase);
            _unitOfWork.Commit();
        }

        public IEnumerable<Purchase> GetAllPurchases(string? searchTerm,string? includeProp = null, int page = 1,int pageSize=1, bool pagination = false, bool descending = false)
        {
            if(searchTerm == null)
            {
                return _purchaseRepository.GetAll(null,includeProp: includeProp,page,pageSize:pageSize,pagination: pagination, u=>u.PurchaseDate,descending);
            }

            searchTerm = searchTerm.ToLower();

            return _purchaseRepository.GetAll(u => u.Supplier.Name.ToLower().Contains(searchTerm) || u.PurchaseDate.ToString("g").ToLower().Contains(searchTerm)
            , includeProp: includeProp,page,pageSize:pageSize, pagination: pagination, u => u.PurchaseDate, descending);
        }

       public int TotalPages(string? searchTerm,int pageSize)
        {
            int totalItems = _purchaseRepository.Count(searchTerm);
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

        public Purchase? GetPurchaseById(int? id, string? includeProp = null)
        {
            return _purchaseRepository.Get(u => u.Id == id,includeProp);
        }

        public void RemovePurchase(Purchase purchase)
        {
            _purchaseRepository.Remove(purchase);
            _unitOfWork.Commit();
        }

        public void UpdatePurchase(Purchase purchase)
        {
            _purchaseRepository.Update(purchase);
            _unitOfWork.Commit();
        }


        public (IEnumerable<SelectListItem> supplierList, IEnumerable<SelectListItem> productList) GetSupplierAndProduct()
        {
            var supplierList = _supplierRepository.GetAll(orderBy: u => u.Name).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            var productList = _productRepository.GetAll(orderBy: u => u.Name).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return (supplierList, productList);
        }
    }
}
