using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;

namespace InventoryManager.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository,IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
        public void AddCategory(Category category)
        {
            _categoryRepository.Add(category);
            _unitOfWork.Commit();
        }

        public IEnumerable<Category> GetAllCategories(string? searchTerm,string? includeProp = null, int page = 1,int pageSize=1,bool pagination=false, bool descending = false)
        {
            if(searchTerm == null)
            {
                return _categoryRepository.GetAll(null,includeProp: includeProp,page,pageSize:pageSize,pagination:pagination,u=>u.Name,descending);
            }

            searchTerm = searchTerm.ToLower();

            return _categoryRepository.GetAll(u=>u.Name.ToLower().Contains(searchTerm),includeProp: includeProp,page,pageSize:pageSize, pagination: pagination, u => u.Name, descending);
        }

       public int TotalPages(string? searchTerm,int pageSize)
        {
            int totalItems = _categoryRepository.Count(searchTerm);
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

        public Category? GetCategoryById(int? id, string? includeProp = null)
        {
            return _categoryRepository.Get(u => u.Id == id,includeProp);
        }

        public void RemoveCategory(Category category)
        {
            _categoryRepository.Remove(category);
            _unitOfWork.Commit();
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
            _unitOfWork.Commit();
        }
    }
}
