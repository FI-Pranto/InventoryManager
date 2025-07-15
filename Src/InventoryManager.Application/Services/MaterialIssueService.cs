using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManager.Application.Services
{
    public class MaterialIssueService : IMaterialIssueService
    {
        private readonly IMaterialIssueRepository _materialIssueRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MaterialIssueService(IMaterialIssueRepository materialIssueRepository,ICustomerRepository customerRepository,IProductRepository productRepository,IUnitOfWork unitOfWork)
        {
            _materialIssueRepository = materialIssueRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public void AddMaterialIssue(MaterialIssue materialIssue)
        {
            _materialIssueRepository.Add(materialIssue);
            _unitOfWork.Commit();
        }

        public IEnumerable<MaterialIssue> GetAllMaterialIssues(string? searchTerm,string? includeProp = null, int page = 1,int pageSize=1, bool pagination = false, bool descending = false)
        {
            if(searchTerm == null)
            {
                return _materialIssueRepository.GetAll(null,includeProp: includeProp,page,pageSize:pageSize,pagination: pagination, u=>u.IssueDate,descending);
            }

            searchTerm = searchTerm.ToLower();

            return _materialIssueRepository.GetAll(u => u.Customer.Name.ToLower().Contains(searchTerm) || u.IssueDate.ToString("g").ToLower().Contains(searchTerm)
            , includeProp: includeProp,page,pageSize:pageSize, pagination: pagination, u => u.IssueDate, descending);
        }

       public int TotalPages(string? searchTerm,int pageSize)
        {
            int totalItems = _materialIssueRepository.Count(searchTerm);
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

        public MaterialIssue? GetMaterialIssueById(int? id, string? includeProp = null)
        {
            return _materialIssueRepository.Get(u => u.Id == id,includeProp);
        }

        public void RemoveMaterialIssue(MaterialIssue materialIssue)
        {
            _materialIssueRepository.Remove(materialIssue);
            _unitOfWork.Commit();
        }

        public void UpdateMaterialIssue(MaterialIssue materialIssue)
        {
            _materialIssueRepository.Update(materialIssue);
            _unitOfWork.Commit();
        }


        public (IEnumerable<SelectListItem> customerList, IEnumerable<SelectListItem> productList) GetCustomerAndProduct()
        {
            var customerList = _customerRepository.GetAll(orderBy: u => u.Name).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            var productList = _productRepository.GetAll(orderBy: u => u.Name).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return (customerList, productList);
        }
    }
}
