using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InventoryManager.Application.Interfaces.IServices
{
    public interface IProductService
    {
        void AddProduct(Product product);
        void UpdateProduct(Product product);

        void RemoveProduct(Product product);

        Product? GetProductById(int? id,string? includeProp=null);

        IEnumerable<Product> GetAllProducts(string? searchTerm,string? includeProp=null,int page=1, int pageSize = 1, bool pagination = false, bool descending = false);

        int TotalPages(string? searchTerm, int pageSize);

        (int startPage, int endPage) GetStartAndEnd(string? searchTerm, int page = 1,int pageSize=1);
        (IEnumerable<SelectListItem> categoryList, IEnumerable<SelectListItem> unitList) GetCategoryAndUnit();

        string ImagePath(IFormFile ImageUri, string myWebRootPath);

        void DeleteImage(string ImagePath, string myWebRootPath);
    }
}
