using InventoryManager.Application.Interfaces.IRepositories;
using InventoryManager.Domain.Entities;
using InventoryManager.Infrastructure.Data;
using InventoryManager.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Infrastructure.Repositories
{
    public class MaterialIssueRepository : Repository<MaterialIssue>, IMaterialIssueRepository
    {
        private readonly ApplicationDbContext _db;

        public MaterialIssueRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int Count(string? searchTerm = null)
        {
            IQueryable<MaterialIssue> query = _db.MaterialIssues.Include(u=>u.Customer);

            if (searchTerm != null)
            {
                query = query.Where(u => u.Customer.Name.ToLower().Contains(searchTerm) || u.IssueDate.ToString("g").ToLower().Contains(searchTerm));
            }
            return query.Count();
            
        }

        public void Update(MaterialIssue item)
        {
            _db.MaterialIssues.Update(item);
        }
    }
}
