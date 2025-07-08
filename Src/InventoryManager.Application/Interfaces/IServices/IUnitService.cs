using InventoryManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Application.Interfaces.IServices
{
    public interface IUnitService
    {
        void AddUnit(Unit unit);
        void UpdateUnit(Unit unit);

        void RemoveUnit(Unit unit);

        Unit? GetUnitById(int id,string? includeProp=null);

        IEnumerable<Unit> GetAllUnits(string? includeProp=null);
    }
}
