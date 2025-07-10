using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;

namespace InventoryManager.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public void AddProduct(Product product)
        {
            _productRepository.Add(product);
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> GetAllProducts(string? searchTerm,string? includeProp = null, int page = 1,int pageSize=1, bool descending = false)
        {
            if(searchTerm == null)
            {
                return _productRepository.GetAll(null,includeProp: includeProp,page,pageSize:pageSize,u=>u.Name,descending);
            }

            searchTerm = searchTerm.ToLower();

            return _productRepository.GetAll(u=>u.Name.ToLower().Contains(searchTerm),includeProp: includeProp,page,pageSize:pageSize, u => u.Name, descending);
        }

       public int TotalPages(string? searchTerm,int pageSize)
        {
            int totalItems = _productRepository.Count(searchTerm);
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

        public Product? GetProductById(int? id, string? includeProp = null)
        {
            return _productRepository.Get(u => u.Id == id,includeProp);
        }

        public void RemoveProduct(Product product)
        {
            _productRepository.Remove(product);
            _unitOfWork.Commit();
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            _unitOfWork.Commit();
        }
    }
}
