using InventoryManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void UpdateCategory(Category category);

        void RemoveCategory(Category category);

        Category? GetCategoryById(int? id,string? includeProp=null);

        IEnumerable<Category> GetAllCategories(string? searchTerm,string? includeProp=null,int page=1, int pageSize = 1, bool pagination = false, bool descending = false);

        int TotalPages(string? searchTerm, int pageSize);

        (int startPage, int endPage) GetStartAndEnd(string? searchTerm, int page = 1,int pageSize=1);
    }
}
