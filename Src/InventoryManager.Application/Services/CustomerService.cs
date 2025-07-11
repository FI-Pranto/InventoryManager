using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Application.Interfaces.IServices;
using InventoryManager.Domain.Entities;

namespace InventoryManager.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository CustomerRepository,IUnitOfWork unitOfWork)
        {
            _customerRepository = CustomerRepository;
            _unitOfWork = unitOfWork;
        }
        public void AddCustomer(Customer customer)
        {
            _customerRepository.Add(customer);
            _unitOfWork.Commit();
        }

        public IEnumerable<Customer> GetAllCustomers(string? searchTerm,string? includeProp = null, int page = 1,int pageSize=1, bool pagination = false, bool descending = false)
        {
            if(searchTerm == null)
            {
                return _customerRepository.GetAll(null,includeProp: includeProp,page,pageSize:pageSize,pagination: pagination, u=>u.Name,descending);
            }

            searchTerm = searchTerm.ToLower();

            return _customerRepository.GetAll(u=>u.Name.ToLower().Contains(searchTerm),includeProp: includeProp,page,pageSize:pageSize, pagination: pagination, u => u.Name, descending);
        }

       public int TotalPages(string? searchTerm,int pageSize)
        {
            int totalItems = _customerRepository.Count(searchTerm);
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

        public Customer? GetCustomerById(int? id, string? includeProp = null)
        {
            return _customerRepository.Get(u => u.Id == id,includeProp);
        }

        public void RemoveCustomer(Customer customer)
        {
            _customerRepository.Remove(customer);
            _unitOfWork.Commit();
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
            _unitOfWork.Commit();
        }
    }
}
