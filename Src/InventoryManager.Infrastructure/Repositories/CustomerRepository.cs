using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Domain.Entities;
using InventoryManager.Infrastructure.Data;
using InventoryManager.Infrastructure.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
             _db = db;
        }
        public int Count(string? searchTerm = null)
        {
            IQueryable<Customer> query = _db.Customers;

            if (searchTerm != null)
            {
                query = query.Where(u => u.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            return query.Count();
        }

        public void Update(Customer customer)
        {
            _db.Customers.Update(customer);
        }
    }
}
