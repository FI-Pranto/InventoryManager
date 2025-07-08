using InventoryManager.Application.Interfaces.IRepositories.Common;
using InventoryManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Application.Interfaces.IRepositories
{
    public interface IUnitRepository : IRepository<Unit>
    {
        void Update(Unit entity);
    }
}
