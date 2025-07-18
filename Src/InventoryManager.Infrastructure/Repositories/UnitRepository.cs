﻿using InventoryManager.Application.Interfaces.IRepositories;
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
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private readonly ApplicationDbContext _db;

        public UnitRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Unit entity)
        {
            _db.Units.Update(entity);
        }
        public int Count(string? searchTerm=null)
        {
            IQueryable<Unit> query = _db.Units;
            if(searchTerm != null)
            {
                query=query.Where(u=>u.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            return query.Count();
            

        }
    }
}
