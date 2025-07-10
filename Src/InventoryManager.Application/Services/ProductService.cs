using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace InventoryManager.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitRepository _unitRepository;
        

        public ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork,ICategoryRepository categoryRepository,IUnitRepository unitRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _unitRepository = unitRepository;
        }
        public void AddProduct(Product product)
        {
            _productRepository.Add(product);
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> GetAllProducts(string? searchTerm,string? includeProp = null, int page = 1,int pageSize=1, bool pagination = false, bool descending = false)
        {
            if(searchTerm == null)
            {
                return _productRepository.GetAll(null,includeProp: includeProp,page,pageSize:pageSize,pagination: pagination, u=>u.Name,descending);
            }

            searchTerm = searchTerm.ToLower();

            return _productRepository.GetAll(u=>u.Name.ToLower().Contains(searchTerm),includeProp: includeProp,page,pageSize:pageSize,pagination :pagination, u => u.Name, descending);
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

        public (IEnumerable<SelectListItem> categoryList, IEnumerable<SelectListItem> unitList) GetCategoryAndUnit()
        {
            var categoryList = _categoryRepository.GetAll(orderBy: u => u.Name).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value=u.Id.ToString()
            });
            var unitList = _unitRepository.GetAll(orderBy: u => u.Name).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return (categoryList, unitList);
        }

        public string ImagePath(IFormFile? ImageUri, string myWebRootPath)
        {
            if (ImageUri == null) return null;
            var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUri.FileName);

            var uploadFolder = Path.Combine(myWebRootPath, "Uploads");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var newPath = Path.Combine(uploadFolder, newFileName);

            using (var stream = new FileStream(newPath, FileMode.Create))
            {
                ImageUri.CopyTo(stream);
            }

            return Path.Combine("Uploads", newFileName);
        }

        public void DeleteImage(string ImagePath,string myWebRootPath)
        {
            var oldPath=Path.Combine(myWebRootPath,ImagePath);

            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
        }
        
    }
}
