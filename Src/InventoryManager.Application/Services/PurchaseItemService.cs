using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;

namespace InventoryManager.Application.Services
{
    public class PurchaseItemService : IPurchaseItemService
    {
        private readonly IPurchaseItemRepository _purchaseItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseItemService(IPurchaseItemRepository purchaseItemRepository,IUnitOfWork unitOfWork)
        {
            _purchaseItemRepository = purchaseItemRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddPurchaseItem(PurchaseItem purchaseItem)
        {
            _purchaseItemRepository.Add(purchaseItem);
            _unitOfWork.Commit();
        }

        public IEnumerable<PurchaseItem> GetAllPurchaseItems(string? searchTerm, string? includeProp = null, int page = 1, int pageSize = 1, bool pagination = false, bool descending = false)
        {
            return _purchaseItemRepository.GetAll(orderBy:u => u.Id);
        }

        public PurchaseItem? GetPurchaseItemById(int? id, string? includeProp = null)
        {
            return _purchaseItemRepository.Get(u=>u.Id==id, includeProp);
        }

        public void RemovePurchaseItem(PurchaseItem purchaseItem)
        {
            _purchaseItemRepository.Remove(purchaseItem);
            _unitOfWork.Commit();
        }

        public void UpdatePurchaseItem(PurchaseItem purchaseItem)
        {
            _purchaseItemRepository.Update(purchaseItem);
            _unitOfWork.Commit();
        }
    }
}
